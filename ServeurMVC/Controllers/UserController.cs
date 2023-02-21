using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class UserController : Controller
{
    private readonly MaDal db;
    private readonly IMapper mapper;

    public UserController(MaDal db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    public IActionResult Index()
    {
        var daos = db.AppUsers;
     var userModels = mapper.Map<IEnumerable<UserModel>>(daos);
        return View(userModels);
    }

    // GET /details/fb525db2-a351-46b6-8f38-4f8f9eecad5a
    public IActionResult Details(Guid id) {
        var user = db.AppUsers.Include(c=>c.Objects).FirstOrDefault(c=>c.IdUser == id);
        if (user == null) {
            return RedirectToAction("index");
        }
        var modelUser = mapper.Map<UserModel>(user);
        return View(modelUser);
    }


}






    // [Authorize(Roles="admin")]
    // public IActionResult Delete(Guid id)
    // {
    //     var dao = db.Users.Find(id);
    //     if (dao == null)
    //     {
    //         return RedirectToAction("index");
    //     }
    //     var idUser = dao.Id;
    //     db.Users.Remove(dao);
    //     return RedirectToAction("index");
    // }