import requests
import time

phyphox_url="http://10.10.25.173"
frequency_path="/get?frequency"
base_frequency=None 

def fetch_frequency():
    try:
        response = requests.get(phyphox_url + frequency_path)
        response.raise_for_status()
        data=response.json()
        frequency=data["buffer"]["frequency"]["buffer"][0]
        return frequency
    except requests.RequestException as excp:
        print(f"Error fetching frequency from Phyphox: {excp}")
        return None

def set_base_frequency():
    try:
        base_frequency=fetch_frequency()
        print(f"Base frequency set to: {base_frequency} Hz")
        return base_frequency
    except KeyError:
        print("Base frequency not found, please check your phyphox setup")
        return None

def main():
    global base_frequency
    base_frequency=set_base_frequency()
    if base_frequency is None:
        print("Unable to fetch frequency from phyphox")
        return
    print("Collecting frequency data from phyphox")
    try:
        while True:
            current_frequency=fetch_frequency()
            if current_frequency is not None:
                difference=current_frequency-base_frequency
                print(f"Frequency difference: {difference} Hz")
            
            time.sleep(0.1)
    except KeyboardInterrupt:
        print("Collecting frequency stopped")

if __name__=="__main__":
    main()
    


