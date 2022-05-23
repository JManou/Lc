// See https://aka.ms/new-console-template for more information

using CurrencyConverter;

const char Separator = ';';
if (args == null || args.Length != 1)
{
    throw new ArgumentException("The path to the input file is expected.");
}

var filePath = args[0];
if (!File.Exists(filePath))
{
    throw new ArgumentException($"File {filePath} not found. Please enter a valid file path");
}

using var fileStream = new FileStream(filePath, FileMode.Open);
using var reader = new StreamReader(fileStream);
var firstLine = reader.ReadLine()?.Split(Separator);
var changeRateCount = Convert.ToInt32(reader.ReadLine()!);

if (firstLine == null || firstLine.Length < 3)
{
    throw new ArgumentException();
}

var from = firstLine[0];
var amount = Convert.ToDecimal(firstLine[1]);
var to = firstLine[2];
var changeRates = new List<ChangeRate>();

while (!reader.EndOfStream && changeRateCount > 0)
{
    var line = reader.ReadLine()!.Split(Separator);
    changeRates.Add(new ChangeRate(line[0], line[1], Convert.ToDecimal(line[2])));
    changeRateCount--;
}

var currencyConverter = new CurrencyConverter.CurrencyConverter(changeRates);
var result = currencyConverter.Convert(from, to, amount);
Console.WriteLine(result);

