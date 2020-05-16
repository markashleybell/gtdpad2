<Query Kind="Program">
  <Reference Relative="..\src\gtdpad\bin\Debug\netcoreapp3.1\gtdpad.dll">C:\Src\gtdpad2\src\gtdpad\bin\Debug\netcoreapp3.1\gtdpad.dll</Reference>
  <Namespace>gtdpad.Domain</Namespace>
  <Namespace>Microsoft.AspNetCore.Identity</Namespace>
</Query>

void Main()
{
    // Guid.NewGuid().Dump();
    
    var user = new User(
        id: new Guid("df77778f-2ef3-49af-a1a8-b1f064891ef5"), 
        email: "testuser@gtdpad.com", 
        password: "test123"
    );
    
    // new PasswordHasher<User>().HashPassword(user, user.Password).Dump();
    
    
}
