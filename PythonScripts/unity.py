import socket 

class UnityCommunicator:

    def __init__(self,host="127.0.0.1",port=25000):
        self.host=host
        self.port=port
    
    def send_frequency_data(self, frequency):
        try:
            with socket.socket(socket.AF_INET, socket.SOCK_STREAM) as s:
                    s.connect((self.host, self.port))  
                    message = f"{frequency}"          
                    s.sendall(message.encode('utf-8')) 
                    print(f"Sent frequency: {frequency} Hz")
        except ConnectionRefusedError:
            print("Connection to Unity failed. Ensure Unity is running and listening on the specified port.")
        except Exception as e:
            print(f"Error sending data to Unity: {e}")
    