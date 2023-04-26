namespace gregslist_cSharp.Repositories;

public class HousesRepository
{
  private readonly IDbConnection _db;
  public HousesRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int CreateHouse(House houseData)
  {
    string sql = @"
    INSERT INTO houses(
      bedrooms, bathrooms, levels, imgUrl, year, description, price, isSold
    )
    VALUES(
      @Bedrooms, @Bathrooms, @Levels, @ImgUrl, @Year, @Description, @Price, @IsSold
    );
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, houseData);
    return id;
  }

  internal List<House> GetAll()
  {
    string sql = "SELECT * FROM houses;";

    List<House> house = _db.Query<House>(sql).ToList();
    return house;
  }

  internal House GetOne(int houseId)
  {
    string sql = "SELECT * FROM houses WHERE id = @houseId;";
    House house = _db.Query<House>(sql, new { houseId }).FirstOrDefault();
    return house;
  }
}
