using FluentValidation;
using mPole.Data.Models;

public class MoveValidator : AbstractValidator<Move>
{
    public MoveValidator()
    {
        RuleFor(move => move.Name).NotEmpty().WithMessage("Nazwa jest obowi¹zkowa");
        RuleFor(move => move.Name).MaximumLength(20).When(move => !string.IsNullOrEmpty(move.Name)).WithMessage("Nazwa nie mo¿e byæ d³u¿sza ni¿ 20 znaków");
        RuleFor(move => move.DifficultyLevel).InclusiveBetween(1, 3).WithMessage("Poziom trudnoœci musi byæ z zakresu od 1 do 3");
        RuleFor(move => move.Description).NotEmpty().WithMessage("Opis jest obowi¹zkowy");
        RuleFor(move => move.Images).NotEmpty().WithMessage("Musisz dodaæ przynajmniej jedno zdjêcie figury");
    }
}