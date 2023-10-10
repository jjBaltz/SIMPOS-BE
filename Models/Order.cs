using System.ComponentModel.DataAnnotations;

namespace SIMPOS.Models;

public class Order
{
	public int OrderId { get; set; }
	public int CustomerId { get; set; }
	public int UserId { get; set; }
	public bool Status { get; set; }
	public string Type { get; set; }
	public string PaymentType { get; set; }
	public decimal Total { get; set; }
	public int Rating { get; set; }
	public Customer Customer { get; set; }
	public List<Item> Items { get; set; }
}

