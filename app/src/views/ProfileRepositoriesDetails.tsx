import { Box, Collapse, IconButton, Paper, Table, TableBody, TableCell, TableContainer, TableHead, TableRow, Typography } from '@mui/material';
import { useEffect, useState } from 'react';
import { ProfileRepositoryDetailsResponse, RuleValidationResult } from '../api/client/ScarecrowClient';
import api from '../api/core/ApiClient';
import { useParams } from 'react-router-dom';
import React from 'react';
import KeyboardArrowDownIcon from '@mui/icons-material/KeyboardArrowDown';
import KeyboardArrowUpIcon from '@mui/icons-material/KeyboardArrowUp';

export default function ProfileRepositoriesDetails() {
  const { id, repoName } = useParams();
  const [repositoryDetails, setRepositoryDetails] = useState<ProfileRepositoryDetailsResponse>();

  const UpdateProfileRepositories = async () => {
    setRepositoryDetails((await api.profileRepositories.getByNameRepoName(id! /* TODO - better way to check param exists */, repoName!)).data);
  };

  useEffect(() => {
    UpdateProfileRepositories();
  }, []);

  return (
    <Box padding="1%">
      {repositoryDetails ? (
        <TableContainer component={Paper}>
          <Table aria-label="collapsible table">
            <TableHead>
              <TableRow>
                <TableCell />
                <TableCell>Name</TableCell>
                <TableCell align="right">Type</TableCell>
                <TableCell align="right">Result</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {repositoryDetails.ruleResults.map((v) => (
                <Row key={v.name} row={v} />
              ))}
            </TableBody>
          </Table>
        </TableContainer>
      ) : (
        'loading...'
      )}
    </Box>
  );
}

function Row(props: { row: RuleValidationResult }) {
  const { row } = props;
  const [open, setOpen] = React.useState(false);

  return (
    <React.Fragment>
      <TableRow sx={{ '& > *': { borderBottom: 'unset' } }}>
        <TableCell>
          <IconButton aria-label="expand row" size="small" onClick={() => setOpen(!open)}>
            {open ? <KeyboardArrowUpIcon /> : <KeyboardArrowDownIcon />}
          </IconButton>
        </TableCell>
        <TableCell component="th" scope="row">
          {row.name}
        </TableCell>
        <TableCell align="right">{row.type}</TableCell>
        <TableCell align="right">{row.isOk ? <span style={{ color: 'green' }}> Ok</span> : <span style={{ color: 'red' }}> Error</span>}</TableCell>
      </TableRow>
      {row.errors && (
        <TableRow>
          <TableCell style={{ paddingBottom: 0, paddingTop: 0 }} colSpan={6}>
            <Collapse in={open} timeout="auto" unmountOnExit>
              <Box sx={{ margin: 1 }}>
                <Typography variant="h6" gutterBottom component="div">
                  Errors list
                </Typography>
                <Table size="small" aria-label="purchases">
                  <TableHead>
                    <TableRow>
                      <TableCell>Error</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {row.errors.map((error, idx) => (
                      <TableRow key={idx}>
                        <TableCell component="th" scope="row">
                          {error}
                        </TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </Box>
            </Collapse>
          </TableCell>
        </TableRow>
      )}
    </React.Fragment>
  );
}
