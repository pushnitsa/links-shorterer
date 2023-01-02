<template>
    <div class="container">
        <form @submit.prevent="onSubmit">
            <div class="col-6 mb-4">
                <label for="inputUrl" class="form-label">Url</label>
                <input
                    type="email"
                    class="form-control"
                    id="inputUrl"
                    aria-describedby="urlHelp"
                />
                <div id="urlHelp" class="form-text">
                    Type an Url address you want the short link to
                </div>
                <div class="mb-3">
                    <label for="inputShortLink" class="form-label">
                        Short link name (optional)
                    </label>
                    <input
                        type="email"
                        class="form-control"
                        id="inputShortLink"
                        aria-describedby="shortLinkHelp"
                    />
                    <div id="shortLinkHelp" class="form-text">
                        Leave this field blank to random short link value
                    </div>
                </div>
                <button type="submit" class="btn btn-primary">
                    Create short link
                </button>
            </div>
        </form>
    </div>
</template>

<script>
export default {
    methods: {
        onSubmit() {},
    },
};
</script>
