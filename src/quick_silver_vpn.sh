#!/bin/bash

api="https://api.quicksilvervpn.com/api/v1"
user_agent="okhttp/3.14.9"
uuid=$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 8)-$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 4)-4$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 3)-$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 4)-$(cat /dev/urandom | tr -dc 'a-f0-9' | head -c 12)

function register() {
	curl --request POST \
		--url "$api/register" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" \
		--data '{
			"model": "RMX3551",
			"brand": "realme",
			"productName": "RMX3551",
			"manufacture": "realme",
			"device": "gracelte",
			"deviceLanguage": "ru",
			"timeZone": "GMT+01:00",
			"appId": "com.quicksilvervpn",
			"screenDensityDpi": 480,
			"screenHeightPx": 1920,
			"screenWidthPx": 1080,
			"deviceSerialNumber": "'$(cat /dev/urandom | tr -dc 'a-zA-Z0-9' | fold -w 16 | head -n 1 | tr '[:upper:]' '[:lower:]')'",
			"networkOperator": "'$(shuf -i 100000-999999 -n 1)'",
			"wgPublicKey": "'$(head -c 43 /dev/urandom | base64 | tr -d '\n' | head -c 43; echo "=")'",
			"uuid": "'$uuid'"
		}'
}

function get_servers() {
	curl --request GET \
		--url "$api/servers?uuid=$uuid" \
		--user-agent "$user_agent" \
		--header "content-type: application/json" 
}
