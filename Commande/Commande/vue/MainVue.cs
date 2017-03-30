using GestionCommande.controleur;
using GestionCommande.model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionCommande.vue
{
    class MainVue
    {
        private Controleur controleur;
        public MainVue(Controleur controleur)
        {
            this.controleur = controleur;
        }

        public void start()
        {
            string input = "";
            do
            {
                Console.WriteLine("Quelle actions souhaitez vous faire : ");
                Console.WriteLine("1 : Afficher la liste des clients");
                Console.WriteLine("2 : Afficher la liste des produits");
                Console.WriteLine("3 : Afficher la liste des commandes");
                Console.WriteLine("4 : Afficher les commandes d'un client");
                Console.WriteLine("5 : Ajouter une commande");
                Console.WriteLine("6 : Ajouter un client");
                Console.WriteLine("7 : Ajouter un produit");
                Console.WriteLine("q : quitter");

                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AfficherClients();
                        break;
                    case "2":
                        AfficherProduits();
                        break;
                    case "3":
                        AfficherCommandes();
                        break;
                    case "4":
                        AfficherCommandeParClient();
                        break;
                    case "5":
                        AjouterCommande();
                        break;
                    case "6":
                        AjouterClient();
                        break;
                    case "7":
                        AjouterProduitVue();
                        break;
                    default:
                        break;
                }
                Console.WriteLine();
            } while (!"q".Equals(input));
        }

        private void AfficherCommandeParClient()
        {
            Client client = this.ChoisirClient();
            this.AfficherCommandes(client.Commandes);
        }

        private void AfficherCommandes()
        {
            this.AfficherCommandes(controleur.GetCommandes());
        }

        private void AfficherCommandes(ICollection<Commande> commandes)
        {
            foreach (Commande commande in commandes)
            {
                Console.WriteLine("Client : " + commande.Client.Prenom + " " + commande.Client.Nom);
                Console.WriteLine("Produits : ");
                foreach (LigneCommande ligneCommande in commande.LignesCommande)
                {
                    Console.WriteLine("Désignation : " + ligneCommande.Produit.Designation);
                    Console.WriteLine("Quantité : " + ligneCommande.Quantite);
                }
                Console.WriteLine();
            }
        }

        private void AfficherProduits()
        {
           foreach(Produit produit in controleur.GetProduits())
            {
                Console.WriteLine("Désignation : " + produit.Designation);
                Console.WriteLine("Prix : "+produit.Prix);
            }
        }

        private void AjouterCommande()
        {
            Console.WriteLine("Création d'une nouvelle commande");

            Client cli = this.ChoisirClient();
            ICollection<LigneCommande> lignesCommande = this.AjouterLignes();

            controleur.CreerCommande(cli, lignesCommande);
        }
        private void AjouterClient()
        {
            Console.WriteLine("Création d'un nouveau client");

            Console.WriteLine("Entrez son nom:");
            string nom= Console.ReadLine();

            Console.WriteLine("Entrez son prenom:");
            string prenom= Console.ReadLine();

            Console.WriteLine("Entrez son mail:");
            string mail = Console.ReadLine();

            Client nouveauClient = new Client();
            nouveauClient.Nom = nom;
            nouveauClient.Prenom = prenom;
            nouveauClient.Mail = mail;

            controleur.CreerClient(nouveauClient);

            Console.WriteLine();
            Console.WriteLine("un nouveau client a été créé avec les informations suivantes: \n nom {0} \n prénom {1} \n mail {2}",nom,prenom,mail);
            
        }
        private void AjouterProduitVue()
        {
            Console.WriteLine("Création d'un nouveau produit");

            Console.WriteLine("Entrez sa désignation:");
            string designation = Console.ReadLine();

            Console.WriteLine("Entrez son prix:");
            int prix = int.Parse(Console.ReadLine());

            

            Produit nouveauProduit = new Produit();
            nouveauProduit.Designation = designation;
            nouveauProduit.Prix = prix;         

            controleur.CreerProduit(nouveauProduit);

            Console.WriteLine();
            Console.WriteLine("un nouveau Produit a été créé avec les informations suivantes: \n désignation {0} \n prix {1} ", designation, prix);

        }
        private Client ChoisirClient()
        {
            Console.WriteLine("Entrez l'ID du client pour la commande : ");
            ICollection<Client> clients = controleur.GetClients();
            this.AfficherClients();
            string input = Console.ReadLine();
            return  clients.Where(c => c.Id == int.Parse(input)).First();
        }

        private ICollection<LigneCommande> AjouterLignes()
        {
            ICollection<LigneCommande> lignesCommande = new Collection<LigneCommande>();
            string input = "";
            do
            {

                Produit prod = this.AjouterProduit();
                Console.WriteLine("Veuillez entrer une quantité");
                int qte = int.Parse(Console.ReadLine());

                LigneCommande ligneCmd = new LigneCommande() { Produit = prod, Quantite = qte };
                lignesCommande.Add(ligneCmd);
                Console.WriteLine("Appuyer sur 'q' si vous souhaitez arrêter d'ajouter des produits, sinon, appuyez sur n'importe quelle touche");
                input = Console.ReadLine();
            } while (!"q".Equals(input));

            return lignesCommande;
        }

        private Produit AjouterProduit()
        {
            Console.WriteLine("Entrez l'ID d'un produit à ajouter à la commande : ");
            ICollection<Produit> produits = controleur.GetProduits();
            foreach (Produit produit in produits)
            {
                Console.WriteLine(produit.Id + " : " + produit.Designation);
            }
            string input = Console.ReadLine();
            return produits.Where(p => p.Id == int.Parse(input)).First();
        }

        private int AjouterQuantité()
        {
            Console.WriteLine("Veuillez entrer une quantité");
            return int.Parse(Console.ReadLine());
        }

        private void AfficherClients()
        {
            foreach (Client client in controleur.GetClients())
            {
                Console.WriteLine(client.Id + " : " + client.Prenom + " " + client.Nom);
            }
        }
    }
}
