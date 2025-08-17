import requests

# Domain name
_host = "example.com"

# Relative Path
_paths = [

    ]
protocol = "https"
_urls = [f"{protocol}://{_host}{path}" for path in _paths]

# IndexNow API Key
_key = ""
_keyloc = f"https://{_host}/{_key}.txt"

def send_indexnow_dual(host: str, urls: list, key: str, key_location: str):
    indexnow_endpoint = "https://api.indexnow.org/indexnow"
    payload_all = {
        "host": host,
        "key": key,
        "keyLocation": key_location,
        "urlList": urls
    }
    resp_all = requests.post(indexnow_endpoint, json=payload_all)
    if resp_all.status_code == 200:
        print("✅ IndexNow-All succeeded")
    else:
        print(f"❌ IndexNow-All failed: {resp_all.status_code}")
        print(resp_all.text)


    bing_endpoint = "https://www.bing.com/indexnow"
    for url in urls:
        params = {
            "url": url,
            "key": key,
            "keyLocation": key_location
        }
        resp_bing = requests.get(bing_endpoint, params=params)
        if resp_bing.status_code == 200:
            print(f"✅ IndexNow-Bing(GET): {url}")
        else:
            print(f"❌ IndexNow-Bing(GET): {url} - {resp_bing.status_code}")
            print(resp_bing.text)

if __name__ == "__main__":
    send_indexnow_dual(
        host=_host,
        urls=_urls,
        key=_key,
        key_location=_keyloc
    )