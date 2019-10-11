using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TaskCancellationByToken.WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet("WithCancellationToken")]
        public ActionResult<IEnumerable<string>> Get(CancellationToken cancellationToken)
        {
            List<string> lst = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                //cancellationToken.ThrowIfCancellationRequested(); //System.OperationCanceledException: 'The operation was canceled.'

                if (cancellationToken.IsCancellationRequested)
                {
                    Debug.WriteLine("Task Cancled");
                    break;
                }
                else
                {
                    var value = "value" + i;
                    lst.Add(value);
                    Debug.WriteLine(value);
                }
                Thread.Sleep(1000);
            }
            Debug.WriteLine("Task Finished");
            return lst;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            List<string> lst = new List<string>();
            for (int i = 1; i <= 10; i++)
            {
                var value = "value" + i;
                lst.Add(value);
                Debug.WriteLine(value);
                Thread.Sleep(1000);
            }
            Debug.WriteLine("Task Finished");
            return lst;
        }

    }
}
