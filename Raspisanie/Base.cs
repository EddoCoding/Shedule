using SQLite;

namespace Raspisanie
{
    public class Base
    {
        private readonly SQLiteAsyncConnection _connection;

        public Base(string dbPath)
        {
            _connection = new SQLiteAsyncConnection(dbPath);
            _connection.CreateTableAsync<ItemCollection>();
            _connection.CreateTableAsync<Couple>();
            _connection.CreateTableAsync<CoupleTime>();
        }

        public Task<List<ItemCollection>> GetData() => _connection.Table<ItemCollection>().ToListAsync();
        public Task<List<Couple>> GetDataCouple() => _connection.Table<Couple>().ToListAsync();
        public Task<List<CoupleTime>> GetDataCoupleTime() => _connection.Table<CoupleTime>().ToListAsync();
        public Task<int> Save(ItemCollection itemCollection) => _connection.InsertAsync(itemCollection);
        public Task<int> Delete(ItemCollection itemCollection) => _connection.DeleteAsync(itemCollection);
        public Task<int> Save(Couple couple) => _connection.InsertAsync(couple);
        public Task<int> Delete(Couple couple) => _connection.DeleteAsync(couple);
        public Task<int> Save(CoupleTime coupleTime ) => _connection.InsertAsync(coupleTime);
        public Task<int> DeleteAll() => _connection.DeleteAllAsync<CoupleTime>();
    }
}
