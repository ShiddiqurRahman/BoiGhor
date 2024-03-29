﻿using BoiGhor.DataAccess.Repository.IRepository;
using BoiGhor.Models;
using BoiGhor.Utility;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoiGhor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {   
            CoverType coverType = new CoverType();
            if (id == null)
            {   //this is for edit
                return View(coverType);
            }
            // this is for edit
            var parameter = new DynamicParameters();
            parameter.Add("@Id", id);
            parameter.Add("@Name", "");
            parameter.Add("@action", "g");
            coverType = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType, parameter);
            if (coverType == null)
            {
                return NotFound();
            }
            return View(coverType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {   
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Id", coverType.Id);
                parameter.Add("@Name", coverType.Name);
               
                if (coverType.Id == 0)
                {
                    //_unitOfWork.CoverType.Add(coverType);
                    
                    parameter.Add("@action", "i");
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType, parameter);
                }
                else
                {
                    parameter.Add("@action", "u");
                    //_unitOfWork.CoverType.Update(coverType);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType, parameter);
                }
                _unitOfWork.Save();
            }

            return RedirectToAction("Index");
        }




        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var parameter = new DynamicParameters();
            parameter.Add("@Id", "");
            parameter.Add("@Name", "");
            parameter.Add("@action", "g");
            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverType,parameter);
            return Json(new { data = allObj });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var parameter = new DynamicParameters();
            var parameter1 = new DynamicParameters();
            parameter.Add("@Id", id);
            parameter.Add("@Name", "");
            parameter.Add("@action", "g");

         
            var objFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType, parameter);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });

            }
            // _unitOfWork.CoverType.Remove(objFromDb);
            parameter1.Add("@Id", id);
            parameter1.Add("@Name", "");
            parameter1.Add("@action", "d");
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType, parameter1);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
