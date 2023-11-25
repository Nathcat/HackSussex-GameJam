using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.IO;
using System;

public class NetworkServer : MonoBehaviour
{
    public class ClientsJoinThread {
        public NetworkServer parent;
        public int worldSeed;

        public ClientsJoinThread(NetworkServer parent, int worldSeed) {
            this.parent = parent;
            this.worldSeed = worldSeed;
        }

        public void Proc() {
            parent.server = new TcpListener(IPAddress.Parse("0.0.0.0"), 10000);
            parent.server.Start();
        
            while (parent.networkState == STATE_ACCEPTING_PLAYERS && parent.pClientsEnd != 6 && parent.runThreads) {
                while (!parent.server.Pending() && parent.networkState == STATE_ACCEPTING_PLAYERS && parent.runThreads) {
                    Debug.Log("Server: Waiting for clients");
                }

                if (parent.networkState != STATE_ACCEPTING_PLAYERS) {
                    break;
                }

                TcpClient client = parent.server.AcceptTcpClient();

                NetworkStream stream = client.GetStream();
                byte[] join_packet = new byte[2];
                stream.Read(join_packet, 0, 2);

                if (join_packet[0] == PACKETTYPE_CLIENT_JOIN) {
                    parent.clients[parent.pClientsEnd] = client;
                    parent.clientIDs[parent.pClientsEnd] = join_packet[1];
                    parent.streams[parent.pClientsEnd] = stream;
                    parent.pClientsEnd++;

                    byte[] resp = new byte[] {
                        PACKETTYPE_CLIENT_JOIN,
                        0x80
                    };

                    if (parent.pClientsEnd == 1) {
                        resp[1] |= 0x1;
                    }

                    stream.Write(resp, 0, 2);
                }
            }

            parent.networkState = STATE_PLAYING;

            // Send the world seed to each client
            for (int i = 0; i < 6; i++) {
                if (parent.clients[i] == null) break;

                NetworkStream stream = parent.clients[i].GetStream();

                byte[] resp = new byte[5] {
                    PACKETTYPE_WORLDGEN,
                    (byte) ((worldSeed >> 24) & 0xFF),
                    (byte) ((worldSeed >> 16) & 0xFF),
                    (byte) ((worldSeed >> 8) & 0xFF),
                    (byte) (worldSeed & 0xFF)
                };

                stream.Write(resp, 0, 5);
            }

            // Send each client information about each other client
            // and create threads for each client
            for (int i = 0; i < 6; i++) {
                if (parent.clients[i] == null) break;

                byte isHunter = 0;
                if (i == 0) {
                    isHunter = 1;
                }

                byte[] resp = new byte[7] {
                    PACKETTYPE_CLIENTSAWARE,
                    0, 0, 0, 0, 0, 0
                };

                for (int a = 0; a < 6; a++) {
                    resp[a+1] = parent.clientIDs[a];
                }

                NetworkStream stream = parent.clients[i].GetStream();
                stream.Write(resp, 0, 7);
                ForwarderThread thr = new ForwarderThread(parent, i);
                Thread t = new Thread(new ThreadStart(thr.Proc));
                t.IsBackground = true;
                t.Start();
            }
        }
    }

    public class ForwarderThread {
        public int i;
        public NetworkServer parent;

        public ForwarderThread(NetworkServer parent, int i) {
            this.i = i;
            this.parent = parent;
        }

        public void Proc() {
            while (parent.networkState == STATE_PLAYING && parent.runThreads) {
                byte[] update = new byte[14];
                parent.streams[i].Read(update, 0, update.Length);

                if (update[0] != PACKETTYPE_FRAME_UPDATE) continue;
    
                // Create the door packet
                GameObject[] doors = GameObject.FindGameObjectsWithTag("door");
                byte[] door_packet = new byte[doors.Length + 5];
                door_packet[0] = PACKETTYPE_DOORUPDATE;
                byte[] length_buffer = BitConverter.GetBytes(doors.Length);
                Buffer.BlockCopy(length_buffer, 0, door_packet, 1, 4);
                for (int i = 0; i < doors.Length; i++) {
                    door_packet[i+5] = doors[i].GetComponent<doorScript>().id;
                }

                // Forward to other clients 
                for (int a = 0; a < 6; a++) {
                    if (a == i) continue;
                    if (parent.clients[a] == null) break;

                    parent.streams[a].Write(update, 0, 14);
                    parent.streams[a].Write(door_packet);
                }
            }
        }
    }

    public const int STATE_ACCEPTING_PLAYERS = 0;
    public const int STATE_PLAYING = 1;
    public const byte PACKETTYPE_CLIENT_JOIN = 0;
    public const byte PACKETTYPE_FRAME_UPDATE = 1;
    public const byte PACKETTYPE_WORLDGEN = 2;
    public const byte PACKETTYPE_CLIENTSAWARE = 3;
    public const byte PACKETTYPE_DOORUPDATE = 4;

    public int networkState = 0;
    private TcpListener server;
    public byte[] clientIDs = new byte[6];
    public TcpClient[] clients = new TcpClient[6];
    public NetworkStream[] streams = new NetworkStream[6];
    private int pClientsEnd = 0;
    private bool runThreads = true;

    void Start() {
        ClientsJoinThread thr = new ClientsJoinThread(this, 1);
        Thread t = new Thread(new ThreadStart(thr.Proc));
        t.IsBackground = true;
        t.Start();
    }

    void OnApplicationQuit() {
        runThreads = false;
        server.Stop();
    }
}
