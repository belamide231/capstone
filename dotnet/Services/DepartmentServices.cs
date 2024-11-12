using Microsoft.AspNetCore.Identity;
using MongoDB.Driver;

public class DepartmentServices {

    private Mongo _Mongo;
    private Redis _Redis;
    private UserManager<ApplicationUser> _UserManager;

    public DepartmentServices(Mongo __Mongo, Redis __Redis, UserManager<ApplicationUser> __UserManager) {
        _Mongo = __Mongo;
        _Redis = __Redis;
        _UserManager = __UserManager;
    }

    public async Task CreateDepartment(CreatingDepartmentDTO DTO, string UserId) {

        // GETTING THE USERS ROLE
        var User = await _UserManager.FindByIdAsync(UserId);
        var Roles = User!.Roles;
        var UserRole = Roles.FirstOrDefault();
        var UserEmail = User.Email;

        // CREATING THE NEW DEPARTMENT
        var NewDepartment = new DepartmentSchema(DTO.DepartmentName!, UserId);
        
        // CHECKING IF THE ROLE IS ADMIN
        if(UserRole == "admin") {

            Console.WriteLine("STORING");

            // STORING IT IN THE DEPARTMENT COLLECTION
            await _Mongo.DepartmentCollection().InsertOneAsync(NewDepartment);
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(NewDepartment, Newtonsoft.Json.Formatting.Indented));

        } else {
        // OR DEAN

            Console.WriteLine("REQUESTING");

            // STORING IT FOR REQUESTING DEPARTMENT CREATION
            var PendingNewDepartment = new PendingDepartmentSchema(NewDepartment, UserEmail!);
            await _Mongo.PendingDepartmentsCollection().InsertOneAsync(PendingNewDepartment);

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(PendingNewDepartment, Newtonsoft.Json.Formatting.Indented));
        }
    }
}