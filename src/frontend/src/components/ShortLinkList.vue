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
                <tr>
                    <td colspan="4" class="text-end">
                        Total count: {{ totalCount }}
                    </td>
                </tr>
            </tbody>
        </table>
        <h5 v-if="!links">Loading...</h5>
    </div>
</template>

<script>
import LinksService from "@/services/LinksService";

function reload(take, skip) {
    return LinksService.getLinks(take, skip);
}

export default {
    props: {
        linksPerPage: {
            type: Number,
            default: 10,
        },
    },
    data() {
        return {
            links: null,
            totalCount: 0,
            followingServer: process.env.VUE_APP_URL_FOLLOWING_SERVER,
        };
    },
    created() {
        reload(this.linksPerPage, 0).then((response) => {
            this.links = response.data.links;
            this.totalCount = response.data.totalCount;
        });
    },
    watch: {
        linksPerPage(newValue) {
            reload(newValue, 0).then((response) => {
                this.links = response.data.links;
            });
        },
    },
};
</script>
