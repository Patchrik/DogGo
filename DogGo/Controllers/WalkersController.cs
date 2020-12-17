using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        private readonly WalkerRepository _walkerRepo;
        private readonly WalksRepository _walksRepo;
        private readonly DogRepository _dogRepo;
        private readonly OwnerRepository _ownerRepo;

        public WalkersController(IConfiguration config)
        {
            _walkerRepo = new WalkerRepository(config);
            _walksRepo = new WalksRepository(config);
            _dogRepo = new DogRepository(config);
            _ownerRepo = new OwnerRepository(config);
        }
        //This is the a helper method to go and grab the current user's id
        private int GetCurrentUserId()
        {
            try
            {
                string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                return int.Parse(id);
            }
            catch (Exception ex)
            {
                return 0;
            }
            
        }

        // GET: WalkersController
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                Owner currentUser = _ownerRepo.GetOwnerById(GetCurrentUserId());
                List<Walker> localWalkers = _walkerRepo.GetWalkersInNeighborhood(currentUser.NeighborhoodId);
                return View(localWalkers);
            }
            else
            {
                List<Walker> walkers = _walkerRepo.GetAllWalkers();
                return View(walkers);
            }
            
        }

        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walksRepo.GetWalksByWalkerId(id);
            List<Dog> dogs = _dogRepo.GetAllDogs();


            if (walker == null)
            {
                return NotFound();
            }
            else
            {
                WalkerProfileViewModel vm = new WalkerProfileViewModel()
                {
                    Walker = walker,
                    Walks = walks,
                    Dogs = dogs
                };
                return View(vm);
            }
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
