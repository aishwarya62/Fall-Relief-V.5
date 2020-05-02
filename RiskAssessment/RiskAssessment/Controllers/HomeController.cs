using RiskAssessment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RiskAssessment.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            var _ctx = new fall_reliefEntities();
            ViewBag.Assessment = _ctx.tbl_RiskAss_Assessment.Where(x => x.IsActive == true).Select(x => new { x.AssessmentTypeID, x.AssessmentType }).ToList();

            SessionModel _model = null;

            if (Session["SessionModel"] == null)
                _model = new SessionModel();
            else
                _model = (SessionModel)Session["SessionModel"];


            return View(_model);
        }


        public ActionResult Instruction(SessionModel model)
        {

            if (model != null) 
            {
                var _ctx = new fall_reliefEntities();
                var assessment = _ctx.tbl_RiskAss_Assessment.Where(x => x.AssessmentTypeID == model.AssessmentTypeID).FirstOrDefault();
                if (assessment != null) 
                {
                    ViewBag.AssessmentType = assessment.AssessmentType;
                    ViewBag.AssessmentDescription = assessment.Description;
                
                }
            }
                Session["SessionModel"] = model;

            if (model == null)
                return RedirectToAction("Index");
            

            return View (model);
        }

        public ActionResult RiskAssessment(SessionModel model)
        {
            if (model != null)
            {
                Session["SessionModel"] = model;
            }

            if (model == null)
            {
                return RedirectToAction("Index");
            }

            var _ctx = new fall_reliefEntities();
            //To create new assessment
            tbl_RiskAss_Session newAssessment = new tbl_RiskAss_Session()
            {
                timeStamp = DateTime.UtcNow,
                AssessmentTypeID = model.AssessmentTypeID,
                sessionID = Guid.NewGuid()

            };
            _ctx.tbl_RiskAss_Session.Add(newAssessment);
            _ctx.SaveChanges();
            this.Session["SessionID"] = newAssessment.sessionID;

            return RedirectToAction("QuestionAssessment", new { @SessionID = Session["SessionID"]});
        }


        public ActionResult QuestionAssessment(Guid SessionID, int? qno)
        {

            if (SessionID == null)
            {
                return RedirectToAction("Index");

            }

            var _ctx = new fall_reliefEntities();

            var asessment = _ctx.tbl_RiskAss_Session.Where(x => x.sessionID.Equals(SessionID)).FirstOrDefault();

            if (asessment == null)
            {
                return RedirectToAction("Index");
            }

            if (qno.GetValueOrDefault() < 1)
                qno = 1;

            var assQuestionID = _ctx.tbl_RiskAss_AssQuestion
                .Where(x => x.AssessmentTypeID == asessment.AssessmentTypeID && x.QuestionNumber == qno)
                .Select(x => x.ID).FirstOrDefault();



            if (assQuestionID > 0)
            {
                var _model = _ctx.tbl_RiskAss_AssQuestion.Where(x => x.ID == assQuestionID)
                    .Select(x => new QuestionModel()
                    {

                        Question = x.tbl_RiskAss_Questions.Question,
                        QuestionNumber = x.QuestionNumber,
                        QuestionType = x.tbl_RiskAss_Questions.AnswerType,
                        AssessmentTypeID = x.AssessmentTypeID,
                        AssessmentType = x.tbl_RiskAss_Assessment.AssessmentType,
                        QuestionSection = x.tbl_RiskAss_Questions.QuestionSection,
                        options = x.tbl_RiskAss_Questions.tbl_RiskAss_ResponseChoice.Select(y => new QRmodel()
                        {
                            responseID = y.ID,
                            response = y.Response
                        }).ToList()
                    }).FirstOrDefault();


                _model.TotalQuestionNo = _ctx.tbl_RiskAss_AssQuestion.Where(x => x.AssessmentTypeID == asessment.AssessmentTypeID).Count();

                return View(_model);

            }

            else
            {

                return View("Error");
            }
            
        }

    }
}