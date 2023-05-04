using pos;
using Xunit.Abstractions;

namespace POS.Test;

public class TerminalTests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public TerminalTests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    private static Dictionary<string, int> GetComparisonDictionary(int example)

    {
        var comparisonDictionary = new Dictionary<string, int>();
        switch (example)
        {
            case 1:
                comparisonDictionary["A"] = 3;
                comparisonDictionary["B"] = 2;
                comparisonDictionary["C"] = 1;
                comparisonDictionary["D"] = 1;
                return comparisonDictionary;
            case 2:
                comparisonDictionary["C"] = 7;
                return comparisonDictionary;
            case 3:
                comparisonDictionary["A"] = 1;
                comparisonDictionary["B"] = 1;
                comparisonDictionary["C"] = 1;
                comparisonDictionary["D"] = 1;
                return comparisonDictionary;

            default:
                return comparisonDictionary;
        }
    }

    private static Dictionary<string, List<Tuple<int, double>>> SetUpDict()
    {
        var priceList = new Dictionary<string, List<Tuple<int, double>>>
        {
            {
                "A", new List<Tuple<int, double>>
                {
                    new(3, 3.0),
                    new(1, 1.25)
                }
            },
            {
                "B", new List<Tuple<int, double>>
                {
                    new(1, 4.25)
                }
            },
            {
                "C", new List<Tuple<int, double>>
                {
                    new(6, 5.0),
                    new(1, 1.0)
                }
            },
            {
                "D", new List<Tuple<int, double>>
                {
                    new(1, 0.75)
                }
            }
        };
        return priceList;
    }

    [Theory]
    [InlineData(0, "")]
    [InlineData(13.25, "ABCDABA")]
    [InlineData(6.0, "CCCCCCC")]
    [InlineData(7.25, "ABCD")]
    public void TestTotalCost(double expected, string input)
    {
        // Arrange
        var terminal = new Terminal();
        var scan = input.ToCharArray();
        terminal.SetPricing(SetUpDict());
        // Act
        foreach (var item in scan) terminal.ScanProduct(item.ToString());

        var actual = terminal.CalculTotal();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData("ABCDABA", 1)]
    [InlineData("CCCCCCC", 2)]
    [InlineData("ABCD", 3)]
    public void TestProductList(string input, int example)
    {
        // Arrange
        var terminal = new Terminal();
        var scan = input.ToCharArray();
        terminal.SetPricing(SetUpDict());
        // Act
        foreach (var item in scan) terminal.ScanProduct(item.ToString());

        var actual = terminal.GetProduceList();
        var expected = GetComparisonDictionary(example);


        // Assert
        Assert.Equal(expected, actual);
    }
}