using Flunt.Notifications;
using Flunt.Validations;

namespace MiniTodo.ViewModels
{
    public class CreateTodoViewModel : Notifiable<Notification>
    {
        public string Title { get; set; }        

        public Todo MapTo()
        {
            var contract = new Contract<Notification>()
            .Requires()
            .IsNotNull(Title, "Enter the title task")
            .IsGreaterThan(Title, 5, "Title must contain more than 5 characters");

            AddNotifications(contract);

            return new Todo(Guid.NewGuid(), Title, false);
        }
    }
}