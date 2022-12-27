<template>
    <CreateShortLinkForm />
    <hr />
    <ShortLinkList />
</template>

<script>
import CreateShortLinkForm from "@/components/CreateShortLinkForm.vue";
import ShortLinkList from "@/components/ShortLinkList.vue";

export default {
    components: { CreateShortLinkForm, ShortLinkList },
};
</script>
