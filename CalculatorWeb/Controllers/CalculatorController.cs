using LabCalculator.Calculator;
using LabCalculator.Dto;

namespace LabCalculator.Controllers;

using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CalculatorController : ControllerBase
{
    private readonly ICalculatorService calculatorService;

    public CalculatorController(ICalculatorService calculatorService)
    {
        this.calculatorService = calculatorService;
    }

    [HttpPost("add")]
    public IActionResult AddToHistory([FromBody] double value)
    {
        return Ok(calculatorService.SetCur(value));
    }

    [HttpPost("step/{step}")]
    public IActionResult SetCurrentToStep(int step)
    {
        var value = calculatorService.ChangeCur(step);
        return Ok(value);
    }

    [HttpPost("operate")]
    public IActionResult Operate([FromBody] OperationDto operationDto)
    {
        double result = calculatorService.Operate(operationDto.Operation, operationDto.Operand);
        return Ok(result);
    }

    [HttpPost("save/{method}")]
    public IActionResult SaveHistory(string method)
    {
        calculatorService.Save(method);
        return Ok();
    }

    [HttpPost("load/{method}")]
    public IActionResult LoadHistory(string method)
    {
        calculatorService.Load(method);
        return Ok();
    }
}