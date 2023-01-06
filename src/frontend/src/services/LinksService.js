import axios from "axios";

const baseURL = process.env.VUE_APP_BASE_URL;

const linksApiService = axios.create({
    baseURL: `${baseURL}/api`,
    withCredentials: false,
    headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
    },
});

export default {
    getLinks(take, skip) {
        return linksApiService.post("/search", { take, skip });
    },
    createLink(fullUrl, shortName = null) {
        return linksApiService.post("/generate", {
            fullUrl,
            shortName,
            isPermanent: true,
        });
    },
};
