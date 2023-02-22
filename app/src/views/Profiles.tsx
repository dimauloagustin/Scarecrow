import { Box } from "@mui/material";
import ProfileCard from "../components/ProfileCard";
import Grid from "@mui/material/Unstable_Grid2";
import { useEffect, useState } from "react";
import { ProfileResponse } from "../api/client/ScarecrowClient";
import api from "../api/core/ApiClient";

export default function Profiles() {
  const [profiles, setProfiles] = useState<ProfileResponse[]>();

  const UpdateProfiles = async () => {
    setProfiles((await api.profiles.get()).data);
  };

  useEffect(() => {
    UpdateProfiles();
  }, []);

  return (
      <Box padding="1%">
        <Grid
          container
          spacing={{ xs: 2, md: 3 }}
          columns={{ xs: 4, sm: 8, md: 12 }}
        >
          {profiles !== undefined
            ? profiles.map((p, index) => (
                <Grid key={index}>
                  <ProfileCard profile={p} />
                </Grid>
              ))
            : "Loading..."}
        </Grid>
      </Box>
  );
}