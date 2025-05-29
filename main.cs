// задание 1 

public interface IPaymentProcessor
{
    void ProcessPayment(decimal amount);
    void RefundPayment(decimal amount);
}

public interface IPaymentValidator
{
    bool ValidatePayment(decimal amount);
}

// Реализация PayPal
public class PayPalProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount) =>
        Console.WriteLine($"PayPal: Оплата {amount} выполнена.");

    public void RefundPayment(decimal amount) =>
        Console.WriteLine($"PayPal: Возврат {amount} выполнен.");
}

// Реализация кредитной карты
public class CreditCardProcessor : IPaymentProcessor, IPaymentValidator
{
    public bool ValidatePayment(decimal amount) => amount < 10000;

    public void ProcessPayment(decimal amount) =>
        Console.WriteLine($"Кредитная карта: Оплата {amount} выполнена.");

    public void RefundPayment(decimal amount) =>
        Console.WriteLine($"Кредитная карта: Возврат {amount} выполнен.");
}

// Реализация криптовалюты
public class CryptoCurrencyProcessor : IPaymentProcessor
{
    public void ProcessPayment(decimal amount) =>
        Console.WriteLine($"Криптовалюта: Оплата {amount} выполнена.");

    public void RefundPayment(decimal amount) =>
        Console.WriteLine($"Криптовалюта: Возврат {amount} выполнен.");
}

// Сервис
public class PaymentService
{
    private readonly IPaymentProcessor processor;
    private readonly IPaymentValidator? validator;

    public PaymentService(IPaymentProcessor processor, IPaymentValidator? validator = null)
    {
        this.processor = processor;
        this.validator = validator;
    }

    public void MakePayment(decimal amount)
    {
        if (validator != null && !validator.ValidatePayment(amount))
        {
            Console.WriteLine("Платеж не прошел проверку!");
            return;
        }
        processor.ProcessPayment(amount);
    }
}
