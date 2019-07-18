namespace WLV.LMS.DAL.Migrations
{
    using BO.Account;
    using BO.Book;
    using BO.Enums;
    using BO.Member;
    using BO.SystemData;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WLV.LMS.DAL.Infrastructure.LMSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override async void Seed(WLV.LMS.DAL.Infrastructure.LMSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            var SystemOptions = new List<SystemOption>()
                  {
                       new SystemOption { Name = MailDetail.SmtpClient.ToString() ,Value="smtp.gmail.com"},
                       new SystemOption { Name = MailDetail.FromEmailId.ToString(),Value="devkuganrajh@gmail.com"},
                       new SystemOption { Name = MailDetail.FromEmailPassword.ToString() ,Value="kugan.pro1"},
                       new SystemOption { Name = MailDetail.PortId.ToString() ,Value="587"}   ,
                       new SystemOption { Name = MailDetail.DisplayName.ToString() ,Value="kuganrajh"}   ,
                       new SystemOption { Name = LibraryOption.LatePaymentDuration.ToString() ,Value="7"},
                       new SystemOption { Name = LibraryOption.ReservationExpireDay.ToString() ,Value="2"}
                  };
            if (!(context.SystemOptions.Count() > 0))
            {
                SystemOptions.ForEach(option => context.SystemOptions.Add(option));
                context.SaveChanges();
            }

            var Roles = new List<IdentityRole>()
                  {
                       new IdentityRole {  Name="SMSClientAdmin"},
                       new IdentityRole { Name = "Member"},
                       new IdentityRole { Name = "Librarian"},
                       new IdentityRole { Name = "Admin"},
                  };

            if (!(context.Roles.Count() > 0))
            {
                Roles.ForEach(Role => context.Roles.Add(Role));
                context.SaveChanges();
            }



            if (!(context.Users.Count() > 0))
            {
                var UserRole = context.Roles.Where(r => r.Name != "Member").ToList();

                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser();
                user.Email = "devkuganrajh@gmail.com";
                user.EmailConfirmed = true;
                // user.PasswordHash = "AC6+4AfRmNFvv3Nf/rn8L3JE9sOcQkp9pCnS2lwmUl7TO2o5SriDhVpWiV27kvBehA==";
                // user.SecurityStamp = "b32fcfcf-1ab5-45a8-8cd1-255b4212e7cb";
                user.UserName = "devkuganrajh@gmail.com";

                manager.Create(user, "kuganRajh12#$");
                UserRole.ForEach(Role => manager.AddToRole(user.Id, Role.Name));

                var store1 = new UserStore<ApplicationUser>(context);
                var manager1 = new UserManager<ApplicationUser>(store1);

                var user1 = new ApplicationUser();
                user1.Email = "qakuganrajh@gmail.com";
                user1.EmailConfirmed = true;
                // user.PasswordHash = "AC6+4AfRmNFvv3Nf/rn8L3JE9sOcQkp9pCnS2lwmUl7TO2o5SriDhVpWiV27kvBehA==";
                // user.SecurityStamp = "b32fcfcf-1ab5-45a8-8cd1-255b4212e7cb";
                user1.UserName = "qakuganrajh@gmail.com";

                manager.Create(user1, "kuganRajh12#$");
                manager1.AddToRole(user1.Id, "SMSClientAdmin");

            }

            if (!(context.Members.Count() > 0))
            {
                // Member 1
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);

                var user = new ApplicationUser();
                user.Email = "kuganrajh@gmail.com";
                user.EmailConfirmed = true;
                user.UserName = "kuganrajh@gmail.com";
                manager.Create(user, "kuganRajh12#$");
                manager.AddToRole(user.Id, "Member");

                var Members = new List<Member>()
                {
                    new Member()
                    {
                        FirstName = "Kuganrajh",
                        City = "Jaffna",
                        Country = "Srilanka",
                        Email = "kuganrajh@gmail.com",
                        LastName = "Rajendran",
                        MobileNumber = "+94765334439",
                        RefNumber = "001",
                        SSID = "943163367v",
                        StreetAddressFirst = "C.C.T.M Lane",
                        StreetAddressSecond = "Kulapitty Road",
                        State = "Kokuvil West",
                        UserId = user.Id,
                        ZipCode = "40000"
                    }
                };

                Members.ForEach(member => context.Members.Add(member));
                context.SaveChanges();


                // Member 2 

                var store2 = new UserStore<ApplicationUser>(context);
                var manager2 = new UserManager<ApplicationUser>(store2);

                var user2 = new ApplicationUser();
                user2.Email = "progamer19941111@gmail.com";
                user2.EmailConfirmed = true;
                user2.UserName = "progamer19941111@gmail.com";
                manager2.Create(user2, "kuganRajh12#$");
                manager2.AddToRole(user2.Id, "Member");

                var Members2 = new List<Member>()
                {
                    new Member()
                    {
                        FirstName = "progamer",
                        City = "Jaffna",
                        Country = "Srilanka",
                        Email = "progamer19941111@gmail.com",
                        LastName = "Gamer",
                        MobileNumber = "+94750409883",
                        RefNumber = "002",
                        SSID = "943163368v",
                        StreetAddressFirst = "C.C.T.M Lane",
                        StreetAddressSecond = "Kulapitty Road",
                        State = "Kokuvil West",
                        UserId = user2.Id,
                        ZipCode = "40000"
                    }
                };

                Members2.ForEach(member => context.Members.Add(member));
                context.SaveChanges();
            }

            if (!(context.Books.Count() > 0))
            {
                var Books = new List<Book>()
                {
                    new Book()
                    {
                        ISBN="9780743298070",
                        Title="ONCE UPON A RIVER",
                        Categories = new List<BO.Category.Category>()
                        {
                            new BO.Category.Category()
                            {
                                Name="Fiction"
                            }
                        },
                        Publisher="ATRIA/EMILY BESTLER BOOKS",
                        PublishedDate="2018-12-04",
                        Description="“One of the most pleasurable and satisfying new books I've read in a long time. Setterfield is a master storyteller...swift and entrancing, profound and beautiful.” —Madeline Miller, internationally bestselling author of Circe and The Song of Achilles “A beguiling tale, full of twists and turns like the river at its heart, and just as rich and intriguing.” —M.L. Stedman, #1 New York Times bestselling author of The Light Between Oceans “This is magical, bewitching storytelling...High prose expressed with rare clarity, story for the unashamed sake of story, a kind of moral dreaminess…well, the list continues to grow.”—Jim Crace, National Book Critics Circle winner and author of Being Dead and Harvest From the instant #1 New York Times bestselling author of the “eerie and fascinating” (USA TODAY) The Thirteenth Tale comes a richly imagined, powerful new novel about the wrenching disappearance of three little girls and the wide-reaching effect it has on their small town. On a dark midwinter’s night in an ancient inn on the river Thames, an extraordinary event takes place. The regulars are telling stories to while away the dark hours, when the door bursts open on a grievously wounded stranger. In his arms is the lifeless body of a small child. Hours later, the girl stirs, takes a breath and returns to life. Is it a miracle? Is it magic? Or can science provide an explanation? These questions have many answers, some of them quite dark indeed. Those who dwell on the river bank apply all their ingenuity to solving the puzzle of the girl who died and lived again, yet as the days pass the mystery only deepens. The child herself is mute and unable to answer the essential questions: Who is she? Where did she come from? And to whom does she belong? But answers proliferate nonetheless. Three families are keen to claim her. A wealthy young mother knows the girl is her kidnapped daughter, missing for two years. A farming family reeling from the discovery of their son’s secret liaison, stand ready to welcome their granddaughter. The parson’s housekeeper, humble and isolated, sees in the child the image of her younger sister. But the return of a lost child is not without complications and no matter how heartbreaking the past losses, no matter how precious the child herself, this girl cannot be everyone’s. Each family has mysteries of its own, and many secrets must be revealed before the girl’s identity can be known. Once Upon a River is a glorious tapestry of a book that combines folklore and science, magic and myth. Suspenseful, romantic, and richly atmospheric, the beginning of this novel will sweep you away on a powerful current of storytelling, transporting you through worlds both real and imagined, to the triumphant conclusion whose depths will continue to give up their treasures long after the last page is turned.",
                        Authors = new List<BO.Author.Author>()
                        {
                            new BO.Author.Author()
                            {
                                Name="Diane Setterfield"
                            }
                        },
                        InfoLink="http://books.google.com/books?id=YpV5DwAAQBAJ&dq=isbn:9780743298070&hl=&source=gbs_api",
                        ImageLink="http://books.google.com/books/content?id=YpV5DwAAQBAJ&printsec=frontcover&img=1&zoom=1&edge=curl&source=gbs_api",
                        BookCurrentCount=10,
                        BookTotalCount=10
                    },

                    new Book()
                    {
                        ISBN="9781250133731",
                        Title="An Anonymous Girl",
                        Categories = new List<BO.Category.Category>()
                        {
                            new BO.Category.Category()
                            {
                                Name="Fiction"
                            }
                        },
                        Publisher="St. Martin's Press",
                        PublishedDate="2019-01-08",
                        Description="The next novel of psychological suspense and obsession from the authors of the blockbuster bestseller The Wife Between Us Seeking women ages 18–32 to participate in a study on ethics and morality. Generous compensation. Anonymity guaranteed. When Jessica Farris signs up for a psychology study conducted by the mysterious Dr. Shields, she thinks all she’ll have to do is answer a few questions, collect her money, and leave. Question #1: Could you tell a lie without feeling guilt? But as the questions grow more and more intense and invasive and the sessions become outings where Jess is told what to wear and how to act, she begins to feel as though Dr. Shields may know what she’s thinking...and what she’s hiding. Question #2: Have you ever deeply hurt someone you care about? As Jess’s paranoia grows, it becomes clear that she can no longer trust what in her life is real, and what is one of Dr. Shields’ manipulative experiments. Caught in a web of deceit and jealousy, Jess quickly learns that some obsessions can be deadly. Question #3: Should a punishment always fit the crime? From the authors of the blockbuster bestseller The Wife Between Us comes an electrifying new novel about doubt, passion, and just how much you can trust someone. Praise for The Wife Between Us: 'A fiendishly smart cat-and-mouse thriller' —New York Times Book Review '[A] seamless thriller that will keep readers on their toes to the very end...Readers will enjoy the dizzying back-and-forth as they attempt to figure out just who to root for and as the suspense ratchets up to one hell of a conclusion.' —Booklist",
                        Authors = new List<BO.Author.Author>()
                        {
                            new BO.Author.Author()
                            {
                                Name="Greer Hendricks",
                            },
                            new BO.Author.Author()
                            {
                                Name="Sarah Pekkanen"
                            }
                        },
                        InfoLink="http://books.google.com/books?id=Ys56DwAAQBAJ&dq=isbn:9781250133731&hl=&source=gbs_api",
                        ImageLink="http://books.google.com/books/content?id=Ys56DwAAQBAJ&printsec=frontcover&img=1&zoom=1&source=gbs_api",
                        BookCurrentCount=10,
                        BookTotalCount=10
                    }
                };

                Books.ForEach(book => context.Books.Add(book));
                context.SaveChanges();
            }
        }
    }
}

