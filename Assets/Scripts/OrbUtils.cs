using System;

public enum OrbColorEnum
{
	RED,
	BLUE,
	YELLOW,
	GREEN
}

public enum OrbSymbolEnum
{
	CIRCLE,
	SQUARE,
	TRIANGLE,
	STAR
}

public enum RingEnum
{
	OUTER,
	INNER,
	ORIGIN
}

public enum DirectionEnum
{
	NORTH,
	NORTHEAST,
	EAST,
	SOUTHEAST,
	SOUTH,
	SOUTHWEST,
	WEST,
	NORTHWEST,
	MIDDLE

}

[Serializable]
public class OrbType : IEquatable<OrbType>
{
	public OrbColorEnum Color;
//	public OrbSymbolEnum Symbol;

	public static bool operator ==(OrbType left, OrbType right)
	{
		return left.Color == right.Color; // && left.Symbol == right.Symbol;
	}

	public static bool operator !=(OrbType left, OrbType right)
	{
		return !(left == right);
	}

	public bool Equals(OrbType other)
	{
		return other == this;
	}

	public override int GetHashCode()
    {
        int hashProductColor = Color.GetHashCode();
//        int hashProductSymbol = Symbol.GetHashCode();
        return hashProductColor; //^ hashProductSymbol;
    }

    public OrbType GetRandomOrbType()
    {
  		OrbColorEnum[] values = (OrbColorEnum[]) Enum.GetValues(typeof(OrbColorEnum));
		OrbType result = new OrbType();
		result.Color = values[new Random().Next(0,values.Length)];

  		return result;
	}    
}

[Serializable]
public class OrbPosition
{
	public RingEnum Ring;
	public DirectionEnum Direction;

	public OrbPosition(RingEnum ring, DirectionEnum direction)
	{
		if (ring == RingEnum.ORIGIN)
		{
			Ring = RingEnum.ORIGIN;
			Direction = DirectionEnum.MIDDLE;
		}
		else
		{
			Ring = ring;
			Direction = direction;
		}
	}
}

public class Orb
{
	public OrbType Type;
	public OrbPosition Position;

	public Orb(OrbType type, OrbPosition position)
	{
		Position = position;
		Type = type;
	}

}

