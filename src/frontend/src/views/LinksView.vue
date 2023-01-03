<template>
    <div class="d-flex justify-content-between pt-3 pb-2 mb-3 border-bottom">
        <h1>Exist links</h1>
        <div class="btn-toolbar mb-2 mb-md-0">
            <div class="btn-group me-2">
                <select class="form-select" v-model="linksPerPage">
                    <option value="10" selected>10</option>
                    <option value="20">20</option>
                    <option value="50">50</option>
                </select>
            </div>
        </div>
    </div>
    <ShortLinkList :links-per-page="+linksPerPage" />
</template>

<script>
import ShortLinkList from "@/components/ShortLinkList.vue";

export default {
    data() {
        return {
            linksPerPage: 10,
        };
    },
    components: { ShortLinkList },
};
</script>
