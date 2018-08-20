using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp1.Models
{

//create table[User](
//Id int not null identity(1,1),
//Email varchar(20) null,
//Constraint PK_User primary key(Id)
//)

//create table[Subscribe](
//Id int not null ,
//Sub_Type int not null,
//Ammount int not null
//Constraint PK_Subscribe primary key(Id, Sub_Type)
//)
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }


    public partial class Blog
    {
        public Blog()
        {
            Post = new HashSet<Post>();
        }

        public int BlogId { get; set; }
        public string Url { get; set; }

        public ICollection<Post> Post { get; set; }
    }


    public partial class Post
    {
        public int PostId { get; set; }
        public int BlogId { get; set; }
        public string Content { get; set; }
        public string Title { get; set; }

        public Blog Blog { get; set; }
    }
}
