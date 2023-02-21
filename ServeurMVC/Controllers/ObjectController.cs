using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ObjectController : Controller
{
    private readonly MaDal db;
    private readonly IMapper mapper;

    public ObjectController(MaDal db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    // GET /Object
    public IActionResult Index(string searchText)
    {
        if (searchText == null)
        {
            searchText = "";
        }
        var daos = db.Objects.Where(c => c.Label.Contains(searchText));
        var objectModels = mapper.Map<IEnumerable<ObjectModel>>(daos);
        return View(objectModels);
    }

    // GET /Object/Details/28032569-2bf4-48ad-9ea6-809c68b2a4e9
    public IActionResult Details(Guid id)
    {
        var obj = db.Objects.Include(c=>c.Photos).Include(c=>c.Owner).FirstOrDefault(c=>c.IdObject == id);

             
        // var obj = db.Objects.Find(id);
        if (obj == null)
        {
            // return NotFound(); // Si je souhaite que l'utilisateur reçoive une erreur 404
            return RedirectToAction("Index");
        }
        var modelObject = mapper.Map<ObjectModel>(obj);
        return View(modelObject);
    }
}