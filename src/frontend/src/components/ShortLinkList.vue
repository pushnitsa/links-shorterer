<template>
    <div class="table-responsive">
        <table class="table table-striped table-sm" v-if="links">
            <thead>
                <tr>
                    <th scope="col">Short name</th>
                    <th scope="col">Url</th>
                    <th scope="col">Hits</th>
                    <th scope="col">Is permanent</th>
                </tr>
            </thead>
            <tbody>
                <tr v-for="link in links" :key="link.id">
                    <td>
                        <a
                            :href="`${followingServer}/${link.shortName}`"
                            target="_blank"
                        >
                            {{ followingServer }}/{{ link.shortName }}
                        </a>
                    </td>
                    <td>{{ link.fullUrl }}</td>
                    <td>{{ link.hits }}</td>
                    <td>{{ link.isPermanent }}</td>
                </tr>
            </tbody>
        </table>
    </div>
</template>

<script>
import LinksService from "@/services/LinksService";

export default {
    data() {
        return {
            links: null,
            followingServer: process.env.VUE_APP_URL_FOLLOWING_SERVER,
        };
    },
    created() {
        LinksService.getLinks(20, 0).then((response) => {
            this.links = response.data.links;
        });
    },
};
</script>
