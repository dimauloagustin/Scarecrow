import { Box } from '@mui/material';
import Grid from '@mui/material/Unstable_Grid2';
import { useEffect, useState } from 'react';
import { ProfileRepositoryResponse } from '../api/client/ScarecrowClient';
import api from '../api/core/ApiClient';
import { useParams } from 'react-router-dom';
import ProfileRepositoryCardResponse from '../components/ProfileRepositoryCard';

export default function ProfileRepositories() {
  const { id } = useParams();
  const [repositories, setRepositories] = useState<ProfileRepositoryResponse[]>();

  const UpdateProfileRepositories = async () => {
    setRepositories((await api.profileRepositories.getByName(id! /* TODO - better way to check param exists */)).data);
  };

  useEffect(() => {
    UpdateProfileRepositories();
  }, []);

  return (
    <Box padding="1%">
      <Grid container spacing={{ xs: 2, md: 3 }} columns={{ xs: 4, sm: 8, md: 12 }}>
        {repositories !== undefined
          ? repositories.map((r, index) => (
              <Grid key={index}>
                <ProfileRepositoryCardResponse profileName={id! /* TODO - better way to check param exists */} repository={r} />
              </Grid>
            ))
          : 'Loading...'}
      </Grid>
    </Box>
  );
}
