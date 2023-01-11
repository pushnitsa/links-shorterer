<template>
    <div class="row" v-if="links && totalCount > linksPerPage">
        <div class="col mb-2">
            <LinkPagination
                :items-count="totalCount"
                :page-size="linksPerPage"
                @navigate="navigateAction"
            />
        </div>
    </div>
    <div class="table-responsive">
        <table class="table table-striped table-sm" v-if="links">
            <thead>
                <tr>
                    <th scope="col">Short link</th>
                    <th scope="col">Url</th>
                    <th scope="col">Created</th>
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
                    <td>{{ formatDate(link.createdAt) }}</td>
                    <td>{{ link.hits }}</td>
                    <td>{{ link.isPermanent }}</td>
                </tr>
                <tr>
                    <td colspan="5" class="text-end">
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
import { formatDate } from "@/helpers/dateFormatter.js";
import LinkPagination from "./LinkPagination.vue";

function reload(take, skip) {
    return LinksService.getLinks(take, skip);
}

export default {
    components: { LinkPagination },
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
    methods: {
        formatDate(date) {
            return formatDate(date);
        },
        navigateAction(data) {
            reload(this.linksPerPage, data.skip).then((response) => {
                this.links = response.data.links;
            });
        },
    },
};
</script>
