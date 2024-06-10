# -*- coding: utf-8 -*-
import sys
import io

# Configura a saída padrão para usar a codificação utf-8
sys.stdout = io.TextIOWrapper(sys.stdout.buffer, encoding='utf-8')

import requests
import json
import pandas as pd

class GraphAPI:
    def __init__(self, ad_acc, fb_api):
        self.base_url = "https://graph.facebook.com/v18.0/"
        self.api_fields = ["spend", "cpc", "cpm", "objective", "adset_name", 
                "adset_id", "clicks", "campaign_name", "campaign_id", 
                "conversions", "frequency", "conversion_values", "ad_name", "ad_id"]
        self.token = "&access_token=" + fb_api

    def get_insights(self, ad_acc, level="campaign"):
        url = self.base_url + "act_" + str(ad_acc)
        url += "?fields=name" #+ level
        #url += "&fields=" + ",".join(self.api_fields)

        data = requests.get(url + self.token)
        data = json.loads(data._content.decode("utf-8"))
        # for i in data["data"]:
        #     if "conversions" in i:
        #         i["conversion"] = float(i["conversions"][0]["value"])
        # return data

    def get_campaigns_status(self, ad_acc):
        url = self.base_url + "act_" + str(ad_acc)
        url += "/campaigns?fields=name,status,adsets{name, id}"
        data = requests.get(url + self.token)
        return json.loads(data._content.decode("utf-8"))

    def get_adset_status(self, ad_acc):
        url = self.base_url + "act_" + str(ad_acc)
        url += "/adsets?fields=name,status,id"
        data = requests.get(url + self.token)
        return json.loads(data._content.decode("utf-8"))

    def get_data_over_time(self, campaign):
        url = self.base_url + str(campaign)
        url += "/insights?fields="+ ",".join(self.api_fields)
        url += "&date_preset=last_30d&time_increment=1"

        data = requests.get(url + self.token)
        data = json.loads(data._content.decode("utf-8"))
        for i in data["data"]:
            if "conversions" in i:
                i["conversion"] = float(i["conversions"][0]["value"])
        return data

if __name__ == "__main__":
    fb_api = "EAAEkP1eRUZBMBOyfoK2cTs1FZAx7msmWovg8nZBPqlqTZCepcXUIGkYa0QNjhCEDHDTOfhk5HSIAcM3HB8aC5QGxhg9HWXL5VU5C5x4zk4C9E8ox0yeghCqiK7U1tnPLjUxUp1WcaBieznoESTx9ACFi0ZCDzrV5TxXvZAcjVGeeyXF1HG7GjQXfZBzJbzoaJdzMvH0DYUkTdZA15ykXRgZDZD"
    ad_acc = "845916937270060"

    graph_api = GraphAPI(ad_acc, fb_api)

    insights_data = graph_api.get_insights(ad_acc)
    print("Insights Data:")
    print(insights_data)

    campaigns_status = graph_api.get_campaigns_status(ad_acc)["data"]
    print("\nCampaigns Status:")
    print(campaigns_status)
