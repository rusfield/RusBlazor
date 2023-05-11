using RusBlazor.Extensions;

long signed = -1;
ulong unsigned = (ulong)signed;
//Console.WriteLine(unsigned);

var flagValues = new List<ulong>();
flagValues.GetFlagsByValue(unsigned);
foreach (var flag in flagValues)
    Console.WriteLine(flag);