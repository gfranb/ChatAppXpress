namespace ChatAppXpress.Models
{
    public class Message
    {
        public int Id { get; set; }
        // Identificadores de usuarios
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        // Propiedades de navegación
        public User Sender { get; set; }
        public User Receiver { get; set; }
        public string Content { get; set; }
        public DateTime DateTime { get; set; }
        public bool IsRead { get; set; }
    }
}
