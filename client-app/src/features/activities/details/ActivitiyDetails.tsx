import { observer } from "mobx-react-lite";
import { useEffect } from "react";
import { useParams } from "react-router";
import { Grid } from "semantic-ui-react";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { useStore } from "../../../app/stores/store";
import ActivityDetaildChats from "./ActivityDetaildChats";
import ActivityDetaildHeader from "./ActivityDetaildHeader";
import ActivityDetaildInfo from "./ActivityDetaildInfo";
import ActivityDetaildSidebar from "./ActivityDetaildSidebar";

export default observer(function ActivityDetails() {
    const {activityStore} = useStore();
    const {selectedActivity: activity, loadActivity, loadingInitial } = activityStore;
    const {id} = useParams<{id: string}>();


    useEffect(() => {
        if (id) loadActivity(id);
    }, [id, loadActivity]);

    if (loadingInitial || !activity) return <LoadingComponent/>;

    return (
        <Grid>
            <Grid.Column width={10}>
                <ActivityDetaildHeader activity={activity} />
                <ActivityDetaildInfo activity={activity} />
                <ActivityDetaildChats />
            </Grid.Column>
            <Grid.Column width={6}>
                <ActivityDetaildSidebar />
            </Grid.Column>
        </Grid>
    )
})