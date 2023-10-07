namespace Dme.Interaction.Models;

public class Id
{
	public int? IntValue { get; private init; }

	public static Id NotSetYet => new() {IntValue = null};

	public Id(int intValue) => IntValue = intValue;

	private Id() => IntValue = null;

	public static implicit operator Id(int intValue) => new() {IntValue = intValue};
	public static implicit operator int(Id id) => id.IntValue ?? 0;
}