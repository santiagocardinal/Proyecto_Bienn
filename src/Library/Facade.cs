namespace Library;

//MI GENTE LATINO
//MISTER WORDWIDE, FIESTA
//DALE

using System;
//using System.Linq;
//using System.Collections.Generic;

    public class Facade
    {
        public static CustomerManager cm = Singleton<CustomerManager>.GetInstance();
        public static SellerManager sm = Singleton<SellerManager>.GetInstance();

        // ---------------------------------------------------------
        //   CREACIÓN DE CLIENTE
        // ---------------------------------------------------------
        public static string CreateCustomer(
            string id, string name, string familyName,
            string mail, string phone, string gender, DateTime birthDate)
        {
            try
            {
                Customer c1 = new Customer(id, name, familyName, mail, phone, gender, birthDate);
                cm.AddCustomer(c1);
                return "Cliente creado correctamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   MODIFICACIÓN DE CLIENTE
        // ---------------------------------------------------------
        public static string ModifyCustomer(string id, string field, string newValue)
        {
            try
            {
                cm.Modify(id, field, newValue);
                return "Cliente modificado correctamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   BÚSQUEDAS
        // ---------------------------------------------------------
        public static string SearchCostumer_ByName(string name)
        {
            try
            {
                return cm.SearchByName(name).Name;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string SearchCostumer_ByFamilyName(string familyname)
        {
            try
            {
                return cm.SearchByFamilyName(familyname).FamilyName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string SearchCostumer_ByPhone(string phone)
        {
            try
            {
                return cm.SearchByPhone(phone).Phone;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string SearchCostumer_ByMail(string mail)
        {
            try
            {
                return cm.SearchByMail(mail).Mail;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   MOSTRAR CLIENTES POR VENDEDOR
        // ---------------------------------------------------------
        public static string ShowCustomers_BySellerId(string sellerId)
        {
            try
            {
                Seller seller = sm.SearchById(sellerId);

                if (seller == null)
                    throw new Exceptions.SellerNotFoundException(sellerId);

                if (seller.Customer.Count == 0)
                    return "El vendedor no tiene clientes asignados.";

                string result = "";
                foreach (Customer customer in seller.Customer)
                {
                    result += $"- {customer.Name}\n";
                }

                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   AGREGAR CUSTOMER
        // ---------------------------------------------------------
        public static string AddCustomer(Customer customer)
        {
            try
            {
                cm.AddCustomer(customer);
                return "Cliente agregado correctamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   AGREGAR TAG A CLIENTE
        // ---------------------------------------------------------
        public static string AddTag_Customer(string customerId, string tagId, string tagName, string tagDescription)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);

                if (customer == null)
                    throw new NotExistingCustomerException();

                if (customer.Tags.Any(t => t.Name.Equals(tagName, StringComparison.OrdinalIgnoreCase)))
                    throw new Exceptions.DuplicateTagException(tagName);

                Tag tag = new Tag(tagId, tagName, tagDescription);
                customer.AddTag(tag);

                return $"Etiqueta '{tagName}' agregada correctamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   NOTAS A INTERACCIONES
        // ---------------------------------------------------------
        public string AddNoteToInteraction(string customerId, string interactionTopic, DateTime interactionDate, Note note)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);

                if (customer == null)
                    throw new NotExistingCustomerException();

                Interaction interaction = customer.Interactions
                    .FirstOrDefault(i => i.Topic == interactionTopic && i.Date == interactionDate);

                if (interaction == null)
                    throw new Exceptions.InteractionNotFoundException();

                interaction.AddNote(note);

                return "Nota agregada correctamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   OBTENER INTERACCIONES
        // ---------------------------------------------------------
        public List<Interaction> GetCustomerInteractions(
            string customerId, string interactionType = null, DateTime? date = null)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);

                if (customer == null)
                    throw new NotExistingCustomerException();

                List<Interaction> interactions = customer.Interactions;

                if (!string.IsNullOrEmpty(interactionType))
                {
                    interactions = interactions
                        .Where(i => i.GetType().Name.Equals(interactionType, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                if (date != null)
                {
                    interactions = interactions
                        .Where(i => i.Date.Date == date.Value.Date)
                        .ToList();
                }

                return interactions;
            }
            catch
            {
                return new List<Interaction>();
            }
        }

        // ---------------------------------------------------------
        //   ÚLTIMA INTERACCIÓN
        // ---------------------------------------------------------
        public static string LastInteraction(string customerId)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);

                if (customer == null)
                    throw new NotExistingCustomerException();

                if (customer.Interactions.Count == 0)
                    return "No hay interacciones registradas.";

                return customer.GetLastInteraction().ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   INTERACCIONES SIN RESPUESTA
        // ---------------------------------------------------------
        public static string UnansweredInteractions(string customerId)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);

                if (customer == null)
                    throw new NotExistingCustomerException();

                var unanswered = customer.GetUnansweredInteractions();

                if (unanswered.Count == 0)
                    return "No hay interacciones sin responder.";

                string report = $"Interacciones sin responder ({unanswered.Count}):\n";
                foreach (var inter in unanswered)
                    report += $"- {inter}\n";

                return report;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   ASIGNAR CLIENTE A VENDEDOR
        // ---------------------------------------------------------
        public static string AssignCustomer(string customerId, string sellerId)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);
                Seller seller = sm.SearchById(sellerId);

                if (customer == null)
                    throw new NotExistingCustomerException();

                if (seller == null)
                    throw new Exceptions.SellerNotFoundException(sellerId);

                cm.AssignCustomerToSeller(customer, seller);

                return "Cliente asignado correctamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   REGISTRO CENTRAL DE INTERACCIONES
        // ---------------------------------------------------------
        private static string RegisterInteraction(Interaction interaction, Customer customer, Seller seller)
        {
            try
            {
                cm.RegisterInteraction(customer, seller, interaction);
                return "Interacción registrada correctamente.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // --------------------------- REGISTROS ---------------------------

        public static string CallRegister(DateTime date, string topic, ExchangeType type, string customerId, string sellerId)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);
                Seller seller = sm.SearchById(sellerId);

                Call call = new Call(date, topic, type, customer);

                return RegisterInteraction(call, customer, seller);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string MeetingRegister(string place, DateTime date, string topic, ExchangeType type, string customerId, string sellerId)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);
                Seller seller = sm.SearchById(sellerId);

                Meeting meeting = new Meeting(place, date, topic, type, customer);

                return RegisterInteraction(meeting, customer, seller);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string MessageRegister(DateTime date, string topic, ExchangeType type, string customerId, string sellerId)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);
                Seller seller = sm.SearchById(sellerId);

                Message message = new Message(date, topic, type, customer);

                return RegisterInteraction(message, customer, seller);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string MailRegister(DateTime date, string topic, ExchangeType type, string customerId, string sellerId)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);
                Seller seller = sm.SearchById(sellerId);

                Mail mailObj = new Mail(date, topic, type, customer);

                return RegisterInteraction(mailObj, customer, seller);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string QuoteRegister(DateTime date, string topic, ExchangeType type, double amount, string description, string customerId, string sellerId)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);
                Seller seller = sm.SearchById(sellerId);

                Quote quote = new Quote(date, topic, type, customer, amount, description);

                return RegisterInteraction(quote, customer, seller);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // ---------------------------------------------------------
        //   SALE DESDE QUOTE
        // ---------------------------------------------------------
        public static string SaleFromQuote(
            string sellerId,
            string customerId,
            DateTime date,
            string topic,
            ExchangeType type,
            double amount,
            string product)
        {
            try
            {
                Customer customer = cm.SearchById(customerId);
                Seller seller = sm.SearchById(sellerId);

                if (customer == null)
                    throw new NotExistingCustomerException();

                if (seller == null)
                    throw new Exceptions.SellerNotFoundException(sellerId);

                Quote foundQuote = customer.Interactions
                    .OfType<Quote>()
                    .FirstOrDefault(q =>
                        q.Date == date &&
                        q.Topic == topic &&
                        q.Type == type &&
                        Math.Abs(q.Amount - amount) < 0.0001);

                if (foundQuote == null)
                    throw new Exceptions.QuoteNotFoundException();

                Sale sale = new Sale(product, foundQuote, date, topic, type, customer);

                return RegisterInteraction(sale, customer, seller);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
