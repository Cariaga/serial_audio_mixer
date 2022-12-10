using System.Runtime.CompilerServices;
using System;
using System.Security.AccessControl;
using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using CSCore.CoreAudioAPI;
using System.Diagnostics;

partial class Program {
  // Create the serial port with basic settings 

  static float X0 = 0F;
  static float X1 = 0F;
  static float X2 = 0F;
  static float X3 = 0F;
  static float X4 = 0F;
  static float X5 = 0F;
  static float X0_old = 0F;
  static float X1_old = 0F;
  static float X2_old = 0F;
  static float X3_old = 0F;
  static float X4_old = 0F;
  static float X5_old = 0F;

  private static AudioSessionManager2 GetDefaultAudioSessionManager2(DataFlow dataFlow) {
    using(var enumerator = new MMDeviceEnumerator()) {
      using(var device = enumerator.GetDefaultAudioEndpoint(dataFlow, Role.Multimedia)) {
        Console.WriteLine("DefaultDevice: " + device.FriendlyName);
        var sessionManager = AudioSessionManager2.FromMMDevice(device);
        return sessionManager;
      }
    }
  }


  private static SerialPort ? port;
  [STAThread]
  static void Main(string[] args) {
    // Instatiate this 
    port = new SerialPort("COM3", 9600, Parity.None, 8, StopBits.One);
   
    // Attach a method to be called when there
    // is data waiting in the port's buffer 
    port.DataReceived += port_OnReceiveData;
    // Begin communications 
    port.Open();


 


    // Enter an application loop to keep this thread alive 
    Console.ReadLine();
    Console.WriteLine("Exit app");
  }
  static void App_vol_control(float vol,string ProcessName) {//the name of application containing
    //Console.WriteLine("thread ID: {0}", Thread.CurrentThread.ManagedThreadId);


    using(var sessionManager = GetDefaultAudioSessionManager2(DataFlow.Render)) {
      using(var sessionEnumerator = sessionManager.GetSessionEnumerator()) {
 
        foreach(var session in sessionEnumerator) {
          var name =session.DisplayName;
        //  Console.WriteLine(name);
      
            using(var simpleVolume = session.QueryInterface < SimpleAudioVolume > ()) {
             

                using var sessionControl = session.QueryInterface<AudioSessionControl2>();
                Console.WriteLine((sessionControl.Process.ProcessName, sessionControl.SessionIdentifier));

                if (Process.GetProcessById(sessionControl.ProcessID).ProcessName.ToLower().Contains(ProcessName.ToLower())){
                 
                
                    simpleVolume.MasterVolume = 1.0f;
                    simpleVolume.MasterVolume = 0.0f;
                    simpleVolume.MasterVolume = vol;
                }else{
                    simpleVolume.IsMuted = false;
                }
              }
        }
      }
    }
  }
 private static void receiveDataLoop(){//
     if(X0!=X0_old){
        float result = LerpMethod.Map(X0, 0F, 127F, 0F, 1F);
        Console.WriteLine(result);
        var t = Task.Run(() => App_vol_control(result,"Opera"));
        t.Wait();
        X0_old=X0;
     }
     if(X1!=X1_old){
        float result = LerpMethod.Map(X1, 0F, 127F, 0F, 1F);
        Console.WriteLine(result);
        var t = Task.Run(() => App_vol_control(result,"Genshin"));
        t.Wait();
        X1_old=X1;
     }
     if(X2!=X2_old){
        float result = LerpMethod.Map(X2, 0F, 127F, 0F, 1F);
        Console.WriteLine(result);
        var t = Task.Run(() => App_vol_control(result,"FireFox"));
        t.Wait();
        X2_old=X2;
     }
     if(X3!=X3_old){
        float result = LerpMethod.Map(X3, 0F, 127F, 0F, 1F);
        Console.WriteLine(result);
        var t = Task.Run(() => App_vol_control(result,"Chrome"));
        t.Wait();
        X3_old=X3;
     }
  }
  // 
  private static void port_OnReceiveData(object sender,
    SerialDataReceivedEventArgs e) {
    SerialPort spL = (SerialPort) sender;
    //  Console.WriteLine(streamReader.ReadLine());
    var splitvalue = port?.ReadExisting()?.Split('-');

    if (splitvalue == null) {
      return;
    }
    var read = splitvalue[0]?.Split(',');
    //   Console.WriteLine(splitvalue[0].ToString());
    if (read == null) {
      return;
    }
    foreach(var item in read) {
      var split_item = item.Split("=");
   
      if (split_item.Length == 2) {
        var key = split_item[0].ToString().ToUpper();
        var value = split_item[1].ToString();
        if (key.Equals("X0")) {
          X0 = float.Parse(value);
        // Console.WriteLine(key +" "+float.Parse(value)+ " "+("X0"==key));
        }
        if (key.Equals("X1")) {
          X1 = float.Parse(value);
       
          // Console.Write(item);
          //  Console.WriteLine();
        }
        if (key.Equals("X2")) {
          X2 = float.Parse(value);
          // Console.Write(item);
          //  Console.WriteLine();
        }
        if (key.Equals("X3")) {
          X3 = float.Parse(value);
          //  Console.Write(item);
          //  Console.WriteLine();
        }
        if (key.Equals("X4")) {
          X4 = float.Parse(value);
          //  Console.Write(item);
          //  Console.WriteLine();
        }
        if (key.Equals("X5")) {
          X5 = float.Parse(value);
          // Console.Write(item);
          // Console.WriteLine();
        }
      }
      //   Console.WriteLine();//to break apart the result  from loop
    }
    receiveDataLoop();
  }
}