namespace RiverSideRestaurant.Contracts;

public interface IOrderable
{
    string InvoiceDetails();

    void MarkAsPreparing();
    
    void MarkAsReady();
    
    void MarkAsServed();
    
    void CancelOrder();
}