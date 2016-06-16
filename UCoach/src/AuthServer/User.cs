namespace AuthServer
{
    /// <summary>
    /// Доменный объект - пользователь
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; private set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string SecondName { get; private set; }

        /// <summary>
        /// Имя учетной записи (логин)
        /// </summary>
        public string Login { get; private set; }   

        /// <summary>
        /// Пароль. Зашифрован bcrypt.
        /// </summary>
        public string Password { get; private set; }

        public User()
        {
        }
    }
}
