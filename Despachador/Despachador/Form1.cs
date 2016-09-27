using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Media;
namespace Despachador
{
    public partial class Form1 : Form
    {
            Consumer consumer;
            public static Queue<String> cola;
            public int estado1, estado2, estado3;
            public Form1()
            {

                InitializeComponent();
                estado1 = 0;
                estado2 = 0;
                estado3 = 0;
                cola= new Queue<String>();
                consumer = new Consumer("192.168.1.6", "hello");
                consumer.onMessageReceived += handleMessage;
                backgroundWorker1.WorkerSupportsCancellation = true;
                backgroundWorker1.RunWorkerAsync();
                ConstructorP1();
                ConstructorP2();
                ConstructorP3();

                dataGridView1.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 19);
                dataGridView2.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 19);
                dataGridView3.DefaultCellStyle.Font = new Font("Microsoft Sans Serif", 19);
                dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 17);
                dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 17);
                dataGridView3.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 17);


            }
           //listen for message events

            public void ConstructorP1() {

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Nombre", typeof(string)));
                dt.Columns.Add(new DataColumn("Cantidad", typeof(string)));
                dt.Columns.Add(new DataColumn("Largo", typeof(string)));
                

                dataGridView1.DataSource = dt;
            }

            public void ConstructorP2()
            {

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Nombre", typeof(string)));
                dt.Columns.Add(new DataColumn("Cantidad", typeof(string)));
                dt.Columns.Add(new DataColumn("Largo", typeof(string)));


                dataGridView2.DataSource = dt;



            }
            public void ConstructorP3()
            {

                DataTable dt = new DataTable();
                dt.Columns.Add(new DataColumn("Nombre", typeof(string)));
                dt.Columns.Add(new DataColumn("Cantidad", typeof(string)));
                dt.Columns.Add(new DataColumn("Largo", typeof(string)));


                dataGridView3.DataSource = dt;

            }
         //delegate to post to UI thread

       private delegate void showMessageDelegate(string message);


       public void Sonar() {

           SoundPlayer player = new SoundPlayer();
            player.SoundLocation = "C:\\Despachador\\alarma.wav";
            player.Play();
       }

       public void SetMessage(String data) {

           string[] msg = data.Split('~');
           int estado = 0;
           foreach (var item in msg)
           {
               String[] prod = item.Split(';');
                if(prod.Length>1){
               if (estado1 == 0)
               {
                   Cliente1.Text = prod[1] + " " + prod[7];
                   vendedor1.Text = prod[2];
                   nventa1.Text = prod[0];
                   direccion1.Text = prod[6];
                   DataTable tabla = (DataTable)dataGridView1.DataSource; ; // Se convierte el DataSource 
               
                   tabla.Rows.Add(prod[3], prod[4], prod[5]);
                   dataGridView1.DataSource = tabla;
                   estado = 1;

               }
               else if (estado2 == 0)
               {

                   Cliente2.Text = prod[1] + " "+ prod[7];
                   Vendedor2.Text = prod[2];
                   nventa2.Text = prod[0];
                   direccion2.Text = prod[6];
                   DataTable tabla = (DataTable)dataGridView2.DataSource; ; // Se convierte el DataSource 

                   tabla.Rows.Add(prod[3], prod[4], prod[5]);
                   dataGridView2.DataSource = tabla;
                   estado = 2;
               }
               else if (estado3 == 0)
               {
                   Cliente3.Text = prod[1] + " " + prod[7];
                   Vendedor4.Text = prod[2];
                   nventa3.Text = prod[0];
                   direccion3.Text = prod[6];
                   DataTable tabla = (DataTable)dataGridView3.DataSource; ; // Se convierte el DataSource 

                   tabla.Rows.Add(prod[3], prod[4], prod[5]);
                   dataGridView3.DataSource = tabla;
                   estado = 3;
               }
               else
               {
                   estado = -1;
               }

               
           }
              
           }

           if(estado==1){
               estado1 = 1;
           }else if(estado==2){
               estado2 = 1;
           }
           else if(estado==3){
               estado3 = 1;
           }else{
               cola.Enqueue(data);
           }
       }


       //Callback for message receive
       public void handleMessage(byte[] message)
       {
           showMessageDelegate s = new showMessageDelegate(SetMessage);
           Sonar();
           string data = System.Text.Encoding.UTF8.GetString(message);
           
           this.Invoke(s, data);
       }
       

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }


        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count-1);
            consumer.StartConsuming();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                consumer.StartConsuming();
                Thread.Sleep(10000);
            }
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            estado1 = 0;
            vendedor1.Text="";
            Cliente1.Text="";
            nventa1.Text = "";
            direccion1.Text = "";
            DataTable tabla = (DataTable)dataGridView1.DataSource;
            tabla.Clear();
            dataGridView1.DataSource = tabla;
            if (cola.Count > 0)
            {
                string val = cola.Dequeue();
                if (val!= null)
                {

                    SetMessage(val);
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            estado2 = 0;
            Vendedor2.Text = "";
            Cliente2.Text = "";
            nventa2.Text = "";
            direccion2.Text = "";
            DataTable tabla = (DataTable)dataGridView2.DataSource;
            tabla.Clear();
            dataGridView2.DataSource = tabla;
            ConstructorP2();
            if (cola.Count > 0)
            {
                string val = cola.Dequeue();
                if (val != null)
                {

                    SetMessage(val);
                }

            }
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            estado3 = 0;
            Vendedor4.Text = "";
            Cliente3.Text = "";
            nventa3.Text = "";
            direccion3.Text = "";
            DataTable tabla=(DataTable)dataGridView3.DataSource;
            tabla.Clear();
            dataGridView3.DataSource = tabla;
            ConstructorP3();
            if (cola.Count > 0)
            {
                string val = cola.Dequeue();
                if (val!= null)
                {

                    SetMessage(val);
                }

            }
            
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.backgroundWorker1.CancelAsync();
            System.Environment.Exit(1);

        }

    }
}
