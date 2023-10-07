using System.ComponentModel.DataAnnotations;

namespace SIMPOS.Models;

public class OrderItem
{
	public int OrderItemId { get; set; }
	public int OrderId { get; set; }
	public Order Order { get; set; }
	public int ItemId { get; set; }
}