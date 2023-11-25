
public static class Extensions
{
    public static ConnectorDirection Inverse(this ConnectorDirection direction)
    {
        return direction switch
        {
            ConnectorDirection.PositiveX => ConnectorDirection.NegativeX,
            ConnectorDirection.PositiveY => ConnectorDirection.NegativeY,
            ConnectorDirection.NegativeX => ConnectorDirection.PositiveX,
            ConnectorDirection.NegativeY => ConnectorDirection.PositiveY,
            _ => throw new System.NotImplementedException(),
        };
    }
}
