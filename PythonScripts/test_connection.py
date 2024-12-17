import requests
import time
import socket

# Phyphox configuration
phyphox_url = "http://10.10.24.54"
frequency_path = "/get?frequency"
base_frequency = None

# Unity configuration
class UnityCommunicator:
    def __init__(self, host="127.0.0.1", port=25000):
        self.host = host
        self.port = port

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

def fetch_frequency():
    try:
        response = requests.get(phyphox_url + frequency_path)
        response.raise_for_status()
        data = response.json()
        frequency = float(data["buffer"]["frequency"]["buffer"][0])
        return frequency
    except requests.RequestException as excp:
        print(f"Error fetching frequency from Phyphox: {excp}")
        return None

def set_base_frequency():
    base_frequency = fetch_frequency()
    if base_frequency is not None:
        print(f"Base frequency set to: {base_frequency:.2f} Hz")
    else:
        print("Failed to set base frequency.")
    return base_frequency

def main():
    global base_frequency
    unity_comm = UnityCommunicator()

    # Set base frequency
    base_frequency = set_base_frequency()
    if base_frequency is None:
        print("Unable to fetch frequency from Phyphox.")
        return

    print("Collecting frequency data from Phyphox and sending to Unity...")
    try:
        while True:
            current_frequency = fetch_frequency()
            if current_frequency is not None:
                difference = current_frequency - base_frequency
                message = f"{difference:.2f}"
                print(f"Frequency difference: {message} Hz")
                unity_comm.send_frequency_data(message)  # Send to Unity
            time.sleep(0.5)
    except KeyboardInterrupt:
        print("Frequency data collection stopped.")

if __name__ == "__main__":
    main()
