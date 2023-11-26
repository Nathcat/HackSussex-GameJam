using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System;

public class NetworkClient : MonoBehaviour
{
    public class ClientSendThread {
        private NetworkClient parent;

        public ClientSendThread(NetworkClient parent) {
            this.parent = parent;
        }

        public void Proc() {
            parent.socket = new TcpClient(parent.serverHostname, 10000);
            parent.stream = parent.socket.GetStream();

            byte[] join_packet = new byte[] {
                NetworkServer.PACKETTYPE_CLIENT_JOIN,
            };

            parent.stream.Write(join_packet);

            byte[] resp = new byte[3];
            parent.stream.Read(resp);
            if (resp[0] != NetworkServer.PACKETTYPE_CLIENT_JOIN) {
                Debug.LogError("Not correct packet type!");
            }

            parent.clientID = resp[1];
            parent.isHunter = (resp[2] & 0x1) == 1;

            // Wait for world gen packet
            resp = new byte[5];
            parent.stream.Read(resp, 0, 5);
            if (resp[0] == NetworkServer.PACKETTYPE_WORLDGEN) {
                parent.worldGenSeed |= (resp[1] << 24);
                parent.worldGenSeed |= (resp[2] << 16);
                parent.worldGenSeed |= (resp[3] << 8);
                parent.worldGenSeed |= resp[4];
            }

            // Wait for client information
            resp = new byte[7];
            parent.stream.Read(resp, 0, 7);
            if (resp[0] == NetworkServer.PACKETTYPE_CLIENTSAWARE) {
                parent.clientIDs = new byte[6];
                parent.hunterID = resp[1];
                for (int i = 0; i < 6; i++) {
                    parent.clientIDs[i] = resp[i+1];
                    
                    if (parent.clientIDs[i] == parent.clientID) continue;

                    for (int a = 0; a < parent.players.Length; a++) {
                        if (parent.players[a].clientID == 0) parent.players[a].clientID = parent.clientIDs[i];
                    }
                }
            }

            ClientRecvThread thr = new ClientRecvThread(parent);
            Thread t = new Thread(new ThreadStart(thr.Proc));
            t.IsBackground = true;
            t.Start();

            parent.connected = true;
        }
    }

    public class ClientRecvThread {
        private NetworkClient parent;

        public ClientRecvThread(NetworkClient parent) {
            this.parent = parent;
        }

        public void Proc() {
            while (parent.runThreads && parent.socket.Connected) {
                while (!parent.stream.DataAvailable && parent.runThreads) {}

                byte[] buffer = new byte[1];
                parent.stream.Read(buffer);
                byte packet_type = buffer[0];

                if (packet_type == NetworkServer.PACKETTYPE_FRAME_UPDATE) {
                    byte[] update = new byte[13];
                    parent.stream.Read(update);
                    byte clientID = update[0];

                    byte[] x = new byte[4];
                    byte[] y = new byte[4];
                    byte[] z = new byte[4];

                    Buffer.BlockCopy(update, 1, x, 0, 4);
                    Buffer.BlockCopy(update, 5, y, 0, 4);
                    Buffer.BlockCopy(update, 9, z, 0, 4);

                    Vector3 position = new Vector3(
                        BitConverter.ToSingle(x),
                        BitConverter.ToSingle(y),
                        BitConverter.ToSingle(z)
                    );

                    for (int i = 0; i < parent.players.Length; i++) {
                        if (parent.players[i] != null && parent.players[i].clientID == clientID) {
                            parent.players[i].position = position;
                        }
                    }
                }
                else if (packet_type == NetworkServer.PACKETTYPE_DOORUPDATE) {
                    byte[] length_buffer = new byte[4];
                    parent.stream.Read(length_buffer);
                    int length = BitConverter.ToInt32(length_buffer);

                    for (int i = 0; i < length; i++) {
                        byte[] door_buffer = new byte[1];
                        parent.stream.Read(door_buffer);
                        GameObject[] doors = GameObject.FindGameObjectsWithTag("door");

                        foreach (GameObject door in doors) {
                            if (door.GetComponent<doorScript>().checkId(door_buffer[0])) door.GetComponent<doorScript>().id = door_buffer[0];
                        }
                    }
                }
                else if (packet_type == NetworkServer.PACKETTYPE_KILL) {
                    byte[] clients = new byte[2];
                    parent.stream.Read(clients);

                    if (clients[1] == parent.clientID) {
                        // TODO The local player was killed
                        parent.stream.Close();
                        parent.alive = false;
                    }
                    else {
                        foreach (NetworkedPlayer player in parent.players) {
                            if (player == null) continue;

                            if (player.clientID == clients[1]) {
                                Debug.Log("Client " + player.clientID + " was killed!");
                                Destroy(player.gameObject);
                            }
                        }
                    }
                }
                else if (packet_type == NetworkServer.PACKETTYPE_TASK_COMPLETE) {
                    buffer = new byte[1];
                    parent.stream.Read(buffer);
                    byte task_id = buffer[0];

                    foreach (int i = 0; i < parent.tasks.Count; i++) {
                        parent.tasks[i].Complete = true;
                    }                    
                }
            }
        }
    }

    private TcpClient socket;
    private NetworkStream stream;
    public string serverHostname = "127.0.0.1";
    public byte clientID;
    public bool isHunter = false;
    public bool connected = false;
    public int worldGenSeed = 0;
    public byte[] clientIDs = new byte[6];
    private bool runThreads = true;
    public NetworkedPlayer[] players = new NetworkedPlayer[6];
    public WorldGenerator worldGenerator;
    private bool worldGenerated = false;
    public bool alive = true;
    public byte hunterID;
    public ArrayList<Task> tasks;

    void Start() {
        clientID = (byte) UnityEngine.Random.Range(0, 255);

        ClientSendThread thr = new ClientSendThread(this);
        Thread t = new Thread(new ThreadStart(thr.Proc));
        t.IsBackground = true;
        t.Start();
    }

    void Update() {
        gameObject.GetComponent<NetworkedPlayer>().clientID = clientID;
        gameObject.GetComponent<NetworkedPlayer>().allowSet = false;

        if (!worldGenerated && worldGenSeed != 0) {
            worldGenerator.Generate(worldGenSeed);
            worldGenerated = true;

            if (isHunter) {
                gameObject.AddComponent<HunterController>();
                gameObject.transform.GetChild(1).gameObject.SetActive(true);
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<ParticleSystem>().Play();
            }
            else {
                gameObject.AddComponent<SurvivorController>();
                gameObject.transform.GetChild(1).gameObject.SetActive(false);
                gameObject.GetComponent<ParticleSystem>().Stop();
            }
        }

        if (connected && alive) {
            // Start sending frame updates
            byte[] update = new byte[] {
                NetworkServer.PACKETTYPE_FRAME_UPDATE,
                clientID,
                0,0,0,0,
                0,0,0,0,
                0,0,0,0
            };

            byte[] x = BitConverter.GetBytes(transform.position.x);
            byte[] y = BitConverter.GetBytes(transform.position.y);
            byte[] z = BitConverter.GetBytes(transform.position.z);

            Buffer.BlockCopy(x, 0, update, 2, 4);
            Buffer.BlockCopy(y, 0, update, 6, 4);
            Buffer.BlockCopy(z, 0, update, 10, 4);

            stream.Write(update, 0, update.Length);
        }
        else if (!alive) {
            gameObject.SetActive(false);
        }
    }

    public void SendKillPacket(NetworkedPlayer player) {
        byte[] packet = new byte[] {
            NetworkServer.PACKETTYPE_KILL,
            clientID,
            player.clientID
        };

        stream.Write(packet);
    }

    public void SendTaskCompletePacket(byte id) {
        byte[] packet = new byte[] {
            NetworkServer.PACKETTYPE_TASK_COMPLETE,
            id
        };
    }

    void OnApplicationQuit() {
        runThreads = false;
        stream.Close();
        socket.Close();
    }
}
