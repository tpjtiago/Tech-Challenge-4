namespace Tech.Challenge4.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Cpf { get; set; }

        public string Phone { get; set; }

        public List<Reserva> Reservas { get; set; } = [];

        public Customer(
            string name,
            string email,
            string cpf,
            string phone
        )
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Nome é obrigatório", name);
            }
            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email é obrigatório", email);
            }
            if (string.IsNullOrWhiteSpace(cpf))
            {
                throw new ArgumentException("Cpf é obrigatório", cpf);
            }
            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Telefone é obrigatório", phone);
            }

            Name = name;
            Email = email;
            Cpf = cpf;
            Phone = phone;
        }
    }
}
