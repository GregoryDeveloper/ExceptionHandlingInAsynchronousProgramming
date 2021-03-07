using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ExceptionHandlingInAsynchronousProgramming.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        public WeatherForecastController()
        {
        }

        [HttpGet("throw-async-exception")]
        public IActionResult GetThrowExcetion()
        {
            try
            {
                ThrowExceptionAsync();
                Console.WriteLine("Continuing");
                return Ok("No Exception was thrown");

            }
            catch (Exception)
            {
                return BadRequest("An exception was thrown");
                throw;
            }
        }

        [HttpGet("throw-async-exception-task")]
        public async Task<IActionResult> GetThrowExcetionTask()
        {
            try
            {
                await ThrowExceptionTaskAsync();
                Console.WriteLine("Continuing");
                return Ok("No Exception was thrown");

            }
            catch (Exception)
            {
                return BadRequest("An exception was thrown");
                throw;
            }
        }

        [HttpGet("throw-async-exception-with-task")]
        public IActionResult GetThrowExcetionWithTask()
        {
            try
            {
                return Ok(ThrowExceptionWithStringTaskAsync());

            }
            catch (Exception)
            {
                return BadRequest("An exception was thrown");
            }
        }

        [HttpGet("throw-async-exception-with-task-awaited")]
        public async Task<IActionResult> GetThrowExcetionWithTaskAwaited()
        {
            try
            {
                await ThrowExceptionWithStringTaskAsync();
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest("An exception was thrown");
            }
        }

        [HttpGet("throw-exception-using-whenall")]
        public async Task<IActionResult> GetThrowExcetionWithWhenAll()
        {
            try
            {
                var task2 = ThrowExceptionTask2Async();
                var task1 = ThrowExceptionTask1Async();

                await Task.WhenAll(task1, task2);
                return Ok(ThrowExceptionWithStringTaskAsync());
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ArgumentException");
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("InvalidOperationException");
            }

            return BadRequest();
        }
        private async void ThrowExceptionAsync()
        {
            await Task.Delay(1);
            throw new InvalidOperationException();
        }

        private async Task ThrowExceptionTaskAsync()
        {
            await Task.Delay(1);
            throw new InvalidOperationException();
        }

        private async Task<string> ThrowExceptionWithStringTaskAsync()
        {
            await Task.Delay(1);
            throw new InvalidOperationException();
        }

        private Task ThrowExceptionTask1Async()
        {
            throw new InvalidOperationException();
        }
        private Task ThrowExceptionTask2Async()
        {
            throw new ArgumentException();
        }
    }
}
