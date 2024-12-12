using FluentValidation;
using mPole.Data.Models;

public class MoveValidator : AbstractValidator<Move>
{
    public MoveValidator()
    {
        RuleFor(move => move.Name).NotEmpty().WithMessage("Nazwa jest obowi�zkowa");
        RuleFor(move => move.Name).MaximumLength(20).When(move => !string.IsNullOrEmpty(move.Name)).WithMessage("Nazwa nie mo�e by� d�u�sza ni� 20 znak�w");
        RuleFor(move => move.DifficultyLevel).InclusiveBetween(1, 3).WithMessage("Poziom trudno�ci musi by� z zakresu od 1 do 3");
        RuleFor(move => move.Description).NotEmpty().WithMessage("Opis jest obowi�zkowy");
        RuleFor(move => move.Images).NotEmpty().WithMessage("Musisz doda� przynajmniej jedno zdj�cie figury");
    }
}