using PersonalProyect.Services.Abtractions;

namespace PersonalProyect.Data.Seeders;

public class MasterSeeder
{
    private readonly DataContext _context;
    private readonly IUserService _userService;
    public MasterSeeder(DataContext context, IUserService _userservice)
    {
        _context = context;
        _userService = _userservice;
    }

    public async Task Run()
    {
        await new CategoriesSeeder(_context).SeedAsync();
        await new BrandsSeeder(_context).SeedAsync();
        await new ProductSeeder(_context).SeedAsync();
        await new PermissionsSeeder(_context).SeedAsync();
        await new UserRolesSeeder(_context, _userService).SeedAsync();
    }
}

