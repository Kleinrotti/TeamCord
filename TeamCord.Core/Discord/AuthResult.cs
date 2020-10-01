namespace TeamCord.Core
{
    public class AuthResult
    {
        public string token { get; protected set; }
        public bool mfa { get; protected set; }
        public string ticket { get; protected set; }

        public string Totp { get; set; }

        public AuthResult(string Token, bool Mfa, string Ticket)
        {
            token = Token;
            mfa = Mfa;
            ticket = Ticket;
        }
    }
}