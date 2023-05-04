using pos;

namespace POS.Test;



public class TerminalTests
{

    public Dictionary<string, List<Tuple<int,double>>> setupDict()
    {
        var priceList = new Dictionary<string, List<Tuple<int,double>>>();
        priceList.Add(
            "A", new List<Tuple<int,double>>
            {
                new (3, 3.0),
                new (1, 1.25)
            }
        );
        priceList.Add(
            "B", new List<Tuple<int,double>>
            {
                new (1, 4.25)
            }
        );
        priceList.Add(
            "C", new List<Tuple<int,double>>
            {
                new (6, 5.0),
                new (1, 1.0)
            }
        );
        priceList.Add(
            "D", new List<Tuple<int,double>>
            {
                new (1, 0.75),
            }
        );
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
        terminal.SetPricing(setupDict());
        // Act
        foreach (var item in scan)
        {
            terminal.ScanProduct(item.ToString());

        }
        double actual = terminal.TotalCost();

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(13.25, "ABCDABA")]
    public void TestPriceList(double expected, string input)
    {
        // Arrange
        var terminal = new Terminal();
        var scan = input.ToCharArray();
        terminal.SetPricing(setupDict());
        // Act
        foreach (var item in scan)
        {
            terminal.ScanProduct(item.ToString());

        }
        double actual = terminal.TotalCost();

        // Assert
        Assert.Equal(expected, actual);
    }
}