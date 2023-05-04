namespace pos;

public class Terminal
{
    private Dictionary<string, List<Tuple<int, double>>> _priceList = new();

    private readonly Dictionary<string, int> _productList = new();

    public double TotalCost()
    {
        var total = 0.0;
        foreach (var product in _productList)
        {
            var amount = product.Value;
            // make sure the price list is in order
            _priceList[product.Key].Sort((x, y) => y.Item1.CompareTo(x.Item1));
            foreach (var deal in _priceList[product.Key])
            {
                var quotient = Math.DivRem(amount, deal.Item1, out var remainder);
                total += quotient * deal.Item2;
                amount = remainder;
            }
        }

        return total;
    }

    public void ScanProduct(string item)
    {
        if (_priceList.ContainsKey(item))
            _productList[item] = _productList.GetValueOrDefault(item, 0) + 1;
        else
            Console.WriteLine("unknown item, please set it aside and ask for help");
    }

    public void SetPricing(Dictionary<string, List<Tuple<int, double>>> priceList)
    {
        _priceList = priceList;
    }
}