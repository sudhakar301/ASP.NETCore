namespace SampleASPDotNetCore.Data
{
    public class MFakeDataStore
    {
        private static List<MProduct> _products;
        public MFakeDataStore()
        {
            _products = new List<MProduct>()
            {
                new MProduct { Id=1, Name="M Product 1 "},
                new MProduct { Id=2, Name="M Product 2 "},
                new MProduct { Id=3, Name="M Product 3 "}
            };
            
        }
        public async Task AddMProduct(MProduct product)
        {
            _products.Add(product);
            await Task.CompletedTask;
        }
        public async Task<IEnumerable<MProduct>> GetAllMProducts()
        {
            return await Task.FromResult(_products);
        }
        public async Task<MProduct> GetMProductbyID(int ID)
        {
            return await Task.FromResult(_products.SingleOrDefault(x=>x.Id==ID));
        }
        public async Task EventOccured(MProduct product,string evnt)
        {
            _products.Single(x => x.Id == product.Id).Name = $"{product.Name} evnt: {evnt}";
            await Task.CompletedTask;
        }
    }
}
