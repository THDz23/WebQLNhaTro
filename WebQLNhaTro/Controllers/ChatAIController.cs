using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;

namespace WebQLNhaTro.Controllers
{
    public class ChatAIController : Controller
    {
        // GET: ChatAI
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult index(string mes)
        {
            string key = "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";
            string answer = string.Empty;
            var openai = new OpenAIAPI(key);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = mes;
            completion.Model = OpenAI_API.Models.Model.DavinciText;
            completion.MaxTokens = 4000;
            var result = openai.Completions.CreateCompletionAsync(completion);
            if (result != null)
            {
                foreach (var item in result.Result.Completions)
                {
                    answer = item.Text;
                }
            }
            ViewBag.Answer = answer;
            ViewBag.Text = mes;
            return View();
        }

        
    }
}